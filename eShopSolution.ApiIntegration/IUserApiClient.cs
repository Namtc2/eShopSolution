using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
	public interface IUserApiClient
	{
		Task<ApiResult<string>> Authenticate(LoginRequest request);
		Task<ApiResult<bool>> RegisterUser(RegisterRequest request);
		Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);
		Task<ApiResult<UserViewModel>> GetById(Guid id);
		Task<ApiResult<bool>> Delete(Guid id);
		Task<ApiResult<bool>> RolesAssign(Guid id, RoleAssignRequest request);
		Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPagings(GetUserPagingRequest request);
	}
}
