using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Catalog.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
	public interface ICategoryApiClient
	{
		Task<ApiResult<List<CategoryViewModel>>> GetAll(string languageId);
	}
}
