﻿using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        Task<PagedResult<UserViewModel>> GetUsersPagings(GetUserPagingRequest request);
    }
}
