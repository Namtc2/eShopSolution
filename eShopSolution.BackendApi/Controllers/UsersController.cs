﻿using eShopSolution.Application.System.Users;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        { 
            _userService = userService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous] //can perform when hasn't login
        public async Task<ActionResult> Authenticate([FromForm] LoginRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var resultToken =await _userService.Authenticate(request);
            if(string.IsNullOrEmpty(resultToken))
                return BadRequest("Username or password is incorrect.");
            return Ok(new {token = resultToken});
        }
        [HttpPost("register")]
        [AllowAnonymous] //can perform when hasn't login
        public async Task<ActionResult> Register([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(request);
            if (!result)
                return BadRequest("Register is unsuccessful.");
            return Ok();
        }
    }
}
