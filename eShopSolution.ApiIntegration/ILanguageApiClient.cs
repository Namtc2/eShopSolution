using eShopSolution.Application.Common;
using eShopSolution.ViewModels.System.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
	public interface ILanguageApiClient
	{
		Task<ApiResult<List<LanguageViewModel>>> GetAll();
	}
}
