using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
                           RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) 
                return null;
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, string.Join(",",roles)),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                    _configuration["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials:creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (request.Keyword == null)
                request.Keyword = "";
            if (string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).
                                   Select(x => new UserViewModel()
                                   {
                                       Email = x.Email,
                                       PhoneNumber = x.PhoneNumber,
                                       UserName = x.UserName,
                                       FirstName = x.FirstName,
                                       Id = x.Id,
                                       LastName = x.LastName
                                   }).ToListAsync();
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<IdentityResult> Register(RegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                Dob = request.Dob,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return result;    
            }
            return result;

        }
    }
}
