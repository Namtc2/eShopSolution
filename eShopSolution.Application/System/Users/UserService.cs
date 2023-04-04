using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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
        private readonly EShopDbContext _context;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                           RoleManager<AppRole> roleManager, IConfiguration configuration, EShopDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ApiErrorResult<string>("Tài khoản không tồn tại");
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Mật khẩu không đúng"); ;
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
                    signingCredentials: creds);
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return new ApiSuccessResult<string>(tokenValue);

        }

        public async Task<ApiResult<bool>> Delete(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("User không tồn tại");
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return new ApiSuccessResult<bool>();
            else
                return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
                return new ApiErrorResult<UserViewModel>("User không tồn tại");
            var roles = await _userManager.GetRolesAsync(user);
            var uvm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                Dob = user.Dob,
                Roles = roles.ToList()
            };
            return new ApiSuccessResult<UserViewModel>(uvm);
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (request.Keyword == null)
                request.Keyword = "";
            if (!string.IsNullOrEmpty(request.Keyword))
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
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var userNam = await _userManager.FindByNameAsync(request.UserName);
            if (userNam != null)
            {
                return new ApiErrorResult<bool>("UserName đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
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
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công (-_-)");

        }

        public async Task<ApiResult<bool>> RoleAssign(Guid Id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var userRolesIds = await _context.UserRoles.Where(x => x.UserId == request.Id).Select(x => x.RoleId).ToListAsync();
            var roles = await _context.Roles.Where(x => userRolesIds.Contains(x.Id)).ToListAsync();

            if (request.Roles != null)
            {
                foreach (var role in request.Roles)
                {
                    if (role.Selected && !userRolesIds.Contains(role.Id))
                    {
                        _context.UserRoles.Add(new IdentityUserRole<Guid>
                        {
                            RoleId = role.Id,
                            UserId = user.Id
                        });

                    }
                    else if (!role.Selected && userRolesIds.Contains(role.Id))
                    {
                        var userRole = await _context.UserRoles.Where(x => x.RoleId == role.Id && x.UserId == user.Id).FirstOrDefaultAsync();
                        if (userRole != null)
                            _context.UserRoles.Remove(userRole);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(Guid Id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != Id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(Id.ToString());

            user.Dob = request.Dob;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<UserViewModel>> Update(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>("User không tồn tại");
            }
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                Dob = user.Dob,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
            return new ApiSuccessResult<UserViewModel>(userVm);
        }
    }
}
