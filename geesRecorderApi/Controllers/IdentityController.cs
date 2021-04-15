﻿using geesRecorderApi.DTOs;
using geesRecorderApi.Models;
using geesRecorderApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderApi.Controllers
{
    [Authorize]
    [Route("auth")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthService _authService;

        public IdentityController(UserManager<ApplicationUser> userManager,
            AuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email); 

            if(user is not null && await _userManager.CheckPasswordAsync(user, password))
            {
                var token = new JwtTokenDTO(_authService.GenerateJSONWebToken(user));
                return Ok(token);
            }

            return Unauthorized(Constants.InvalidLoginCredentials);
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            string userName = User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            var user = await _userManager.FindByNameAsync(userName);
            var token = new JwtTokenDTO(_authService.GenerateJSONWebToken(user));
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var token = new JwtTokenDTO(_authService.GenerateJSONWebToken(user));
                return Ok(token);
            }

            string errors = string.Join(",", result.Errors);
            return BadRequest(errors);
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test() => Ok("dfbgnmbvcd");
    }
}