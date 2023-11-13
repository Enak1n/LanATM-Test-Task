﻿using IdentityAPI.Models.Requests;
using IdentityAPI.Service.Auth.Interfaces;
using IdentityAPI.Service.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IdentityAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _authService.Register(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            try
            {
                var response = await _authService.Login(login, password);

                return Ok(response);
            }
            catch (SecurityTokenException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}