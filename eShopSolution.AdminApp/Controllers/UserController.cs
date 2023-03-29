﻿using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var sessions = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                BrearToken = sessions,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagings(request);
            return View(data);
        }       
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            var result = await _userApiClient.RegisterUser(request);
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View(request);
        }
      
    }
}
