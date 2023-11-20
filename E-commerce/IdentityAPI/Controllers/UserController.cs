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
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get user from data base by Id
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <returns>Status about getting user</returns>
    /// <response code="200">Return user</response>
    /// <response code="404">User not found!</response>
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Get access token
    /// </summary>
    /// <param name="model">Refresh token</param>
    /// <returns>Status about getting</returns>
    /// <response code="200">Return access token</response>
    /// <response code="403">Unauthorized access</response>
    /// <response code="401">Unauthorized access</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="model">User</param>
    /// <returns>Status about registration</returns>
    /// <response code="200">Return 200 status</response>
    /// <response code="400">Return list of errors</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Register new courier
    /// </summary>
    /// <param name="model">Courier</param>
    /// <returns>Status about registration</returns>
    /// <response code="200">Return 200 status</response>
    /// <response code="400">Return list of errors</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Login in system
    /// </summary>
    /// <param name="model">Login model</param>
    /// <returns>Status about login</returns>
    /// <response code="200">Return access and refresh token</response>
    /// <response code="400">Return error</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Logout from system
    /// </summary>
    /// <returns>No content</returns>
    /// <response code="204">It means that you successfully logout from system</response>
    /// <response code="401">It means that you didn't authorize</response>
    [HttpPost]
    [Authorize(Policy = "Public")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        var user = HttpContext.Items["User"] as User;
        await _userService.Logout(new Guid(user.Id));
        return NoContent();
    }
}
