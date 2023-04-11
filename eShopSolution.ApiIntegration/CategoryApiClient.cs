using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
	public class CategoryApiClient : BaseApiClient, ICategoryApiClient
	{
		public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
			: base(httpClientFactory, configuration, httpContextAccessor)
		{

		}
		public async Task<ApiResult<List<CategoryViewModel>>> GetAll(string languageId)
		{
			var data = await GetAsync<ApiResult<List<CategoryViewModel>>>($"/api/categories?languageId={languageId}");
			return data;
		}
	}
}
