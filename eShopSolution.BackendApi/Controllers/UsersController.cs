using eShopSolution.Application.System.Users;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        { 
            _userService = userService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous] //can perform when hasn't login
        public async Task<ActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var resultToken =await _userService.Authenticate(request);
            if(string.IsNullOrEmpty(resultToken.Data))
                return BadRequest(resultToken);            
            return Ok(resultToken);
        }
        [HttpPost]
        [AllowAnonymous] //can perform when hasn't login
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {               
                return BadRequest(result);
            }               
            return Ok(result);
        }
        //PUT http://localhost/api/user/id
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        //http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetUserPagingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = await _userService.GetUsersPaging(request);
            return Ok(users);
        }
        //http://localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var users = await _userService.GetById(id);
            return Ok(users);
        }

    }
}
