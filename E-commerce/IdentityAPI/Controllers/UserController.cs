using AutoMapper;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using IdentityAPI.DataBase.Entities;
using IdentityAPI.Models.Requests;
using IdentityAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using IdentityAPI.Models.DTO.Requests;

namespace IdentityAPI.Controllers;


[ApiController]
[Route("IdentityAPI/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("{userId:Guid}")]
    [Authorize(Policy = "Public")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        try
        {
            var user = await _userService.GetById(userId);
            var response = _mapper.Map<UserDTOResponse>(user);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> GetAccessToken(GetAccessTokenDTORequest model)
    {
        try
        {
            var response = await _userService.GetAccessToken(model.RefreshToken);
            return Ok(response);
        }
        catch (SecurityException ex)
        {
            return Forbid(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegisterDTORequest model)
    {
        var user = _mapper.Map<User>(model);
        var role = Enum.GetName(typeof(Role), model.Role);
        var result = await _userService.Register(user, model.Password, (Role)Enum.Parse(typeof(Role), role));

        if (result.GetType() == typeof(ErrorsResponse))
        {
            return BadRequest(result);
        }

        await _userService.Store.Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RegistrationOfCourier(RegisterCourierDTORequest model)
    {
        var user = _mapper.Map<User>(model);
        var result = await _userService.Register(user, model.Password, Role.Courier);

        if (result.GetType() == typeof(ErrorsResponse))
        {
            return BadRequest(result);
        }

        await _userService.Store.Context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDTORequest model)
    {
        try
        {
            var response = await _userService.Login(model);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Policy = "Public")]
    public async Task<IActionResult> Logout()
    {
        var user = HttpContext.Items["User"] as User;
        await _userService.Logout(new Guid(user.Id));
        return NoContent();
    }
}
