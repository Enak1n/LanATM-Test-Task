﻿using IdentityAPI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using IdentityAPI.DataBase.Entities;
using IdentityAPI.DataBase;
using AutoMapper;
using IdentityAPI.Models.Requests;
using IdentityAPI.Models.Responses;
using MassTransit;
using Infrastructure;
using Infrastructure.DTO;

namespace IdentityAPI.Services
{
    public class UserService : UserManager<User>, IUserService
    {
        private readonly IConfiguration _configuration;

        private readonly IBusControl _bus;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMapper _mapper;

        public new ICustomUserStore Store { get; init; }

        public UserService(IConfiguration configuration,
                            IBusControl bus,
                            ICustomUserStore store,
                            IOptions<IdentityOptions> optionsAccessor,
                            IPasswordHasher<User> passwordHasher,
                            IEnumerable<IUserValidator<User>> userValidators,
                            IEnumerable<IPasswordValidator<User>> passwordValidators,
                            ILookupNormalizer keyNormalizer,
                            IdentityErrorDescriber errors,
                            IServiceProvider services,
                            IMapper mapper,
                            ILogger<UserManager<User>> logger,
                            RoleManager<IdentityRole> roleManager) : base(store,
                                                                      optionsAccessor,
                                                                      passwordHasher,
                                                                      userValidators,
                                                                      passwordValidators,
                                                                      keyNormalizer,
                                                                      errors,
                                                                      services,
                                                                      logger)
        {
            _configuration = configuration;
            _bus = bus;
            Store = store;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new Exception($"User with this ID {id} wasn't founded");
            }

            return user;
        }

        public async Task<LoginDTOResponse> Login(LoginDTORequest model)
        {
            var user = await FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new Exception($"User with this Email {model.Email} wasn't founded");
            }

            return await Login(user, model.Password);
        }

        public async Task<IResponse> Register(User user, string password, Role role)
        {
            var userRole = new IdentityRole { Name = Enum.GetName(typeof(Role), role) };
            var result = await CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new ErrorsResponse { Errors = result.Errors };
            }

            if (userRole.Name == Role.Buyer.ToString())
            {
                await CreateCart(new Guid(user.Id));
            }

            if (userRole.Name == Role.Courier.ToString())
            {
                await CreateCourier(user);
            }

            try
            {
                await AddToRoleAsync(user, userRole.Name);
            }
            catch (InvalidOperationException)
            {
                await _roleManager.CreateAsync(userRole);
                await AddToRoleAsync(user, userRole.Name);
            }

            var claims = new List<Claim>()
            {
            new Claim("UserId", user.Id),
            new Claim(ClaimTypes.Role, userRole.Name)
            };

            await AddClaimsAsync(user, claims);

            return await GenerateTokens(new Guid(user.Id), userRole.Name, claims);
        }

        public async Task<LoginDTOResponse> GetAccessToken(string refreshToken)
        {
            var validatedToken = JwtTokenManager.ValidateToken(_configuration, refreshToken);

            var userId = new JwtSecurityToken(validatedToken).Claims.ToList().FirstOrDefault(x => x.Type == "UserId");


            if (userId == null)
            {
                throw new SecurityException("Incorrect refreshToken");
            }

            if (!await TokenIsActive(refreshToken))
            {
                throw new UnauthorizedAccessException("Unauthorization");
            }

            var user = await FindByIdAsync(userId.Value);

            if (user == null)
            {
                throw new SecurityException("Incorrect refreshToken");
            }

            var roles = await GetRolesAsync(user);

            if (roles.Count == 0)
            {
                throw new SecurityException("Incorrect refreshToken");
            }

            return await GenerateTokens(new Guid(userId.Value), roles[0]);
        }

        public async Task Logout(Guid userId)
        {
            var blockedTokens = await Store.BlockTokens(userId);

            if (blockedTokens.Count > 0 && blockedTokens[0] != null)
            {
                await SendMessageAboutDeletingToken(blockedTokens[0].Value);
            }
        }

        public async Task<bool> TokenIsActive(string token)
        {
            return (await Store.GetToken(token)).IsActive;
        }

        private async Task CreateCart(Guid userId)
        {
            await RabbitMQClient.Request<CartDTORabbitMQ>(_bus, new(userId),
                new($"{_configuration["RabbitMQ:Host"]}/createCartQueue"));
        }

        private async Task CreateCourier(User user)
        {
            var courier = _mapper.Map<CourierDTORabbitMQ>(user);

            await RabbitMQClient.Request(_bus, courier,
                new($"{_configuration["RabbitMQ:Host"]}/createCourierQueue"));
        }

        private async Task SendMessageAboutAddingToken(string token)
        {
            await RabbitMQClient.Request<TokenDTORAbbitMQ>(_bus, new() { Value = token },
                new($"{_configuration["RabbitMQ:Host"]}/addTokenQueue"));
        }

        private async Task SendMessageAboutDeletingToken(string tokenValue)
        {
            await RabbitMQClient.Request<TokenDTORAbbitMQ>(_bus, new() { Value = tokenValue },
                new($"{_configuration["RabbitMQ:Host"]}/deleteTokenQueue"));
        }

        private async Task<LoginDTOResponse> Login(User user, string password)
        {
            if (!await CheckPasswordAsync(user, password))
            {
                throw new Exception("Incorrect password");
            }

            var roles = await GetRolesAsync(user);

            return await GenerateTokens(new Guid(user.Id), roles[0]);
        }

        private async Task<LoginDTOResponse> GenerateTokens(Guid userId, string roleName, List<Claim>? claims = null)
        {
            if (claims == null)
            {
                claims = new List<Claim>()
            {
                new Claim("UserId", userId.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };
            }

            var refreshToken = JwtTokenManager.GenerateJwtRefreshToken(_configuration, new List<Claim>() { claims[0] });
            var accessToken = JwtTokenManager.GenerateJwtAccessToken(_configuration, claims);
            await SendMessageAboutAddingToken(accessToken);

            var blockedTokens = await Store.BlockTokens(userId);

            if (blockedTokens.Count > 0 && blockedTokens[0] != null)
            {
                await SendMessageAboutDeletingToken(blockedTokens[0].Value);
            }

            await Store.AddRangeTokenAsync(new List<Token>()
            {
            new Token()
            {
                UserId = userId,
                TokenType = TokenType.Access,
                Value = accessToken
            },

            new Token()
            {
                UserId = userId,
                TokenType = TokenType.Refresh,
                Value = refreshToken
            }
            });

            return new LoginDTOResponse(accessToken, refreshToken, userId);
        }
    }
}
