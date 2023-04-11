using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
	public interface IProductApiClient
	{
		Task<ApiResult<PagedResult<ProductViewModel>>> GetPagings(GetManageProductPagingRequest request);
		Task<ApiResult<ProductViewModel>> CreateProduct(ProductCreateRequest request);
	}
}
