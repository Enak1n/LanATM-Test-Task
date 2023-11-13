using AutoMapper;
using IdentityAPI.Domain.Entities;
using IdentityAPI.Interfaces;
using IdentityAPI.Models.Requests;
using IdentityAPI.Service.Auth.Interfaces;
using IdentityAPI.Service.Models.Requests;
using IdentityAPI.Service.Models.Responses;
using IdentityAPI.Service.TokenService;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace IdentityAPI.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private IPasswordHasher _passwordHasher;
        private ITokenService _tokenService;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponse> Login(string login, string password)
        {
            var user = await _unitOfWork.Users.Find(u => u.Login == login);

            if (user == null)
            {
                throw new SecurityTokenException("Invalid Email Address or password!");
            }

            if (_passwordHasher.Verify(user.Password, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                };

                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                var access = new Token
                {
                    UserId = user.Id,
                    TokenType = TokenType.Access,
                    TokenValue = accessToken,
                    TimeOfExpired = DateTime.UtcNow.AddMinutes(10)
                };

                var refresh = new Token
                {
                    UserId = user.Id,
                    TokenType = TokenType.Refresh,
                    TokenValue = refreshToken,
                    TimeOfExpired = DateTime.UtcNow.AddDays(15)
                };

                await _unitOfWork.Tokens.Add(access);
                await _unitOfWork.Tokens.Add(refresh);

                await _unitOfWork.SaveChangesAsync();
                return new AuthenticateResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            else
            {
                throw new SecurityTokenException("Invalid Email address or password!");
            }
        }

        public async Task Register(UserRegistration user)
        {
            var emailAddress = await _unitOfWork.Users.Find(u => u.Login == user.Login);

            if (emailAddress != null)
            {
                throw new Exception("Email Address has been already used!");
            }

            var customer = _mapper.Map<UserRegistration, User>(user);
            var hash = _passwordHasher.Hash(user.Password);
            customer.Password = hash;
            customer.CreatedAtDateUtc = DateTime.UtcNow;

            await _unitOfWork.Users.Add(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
