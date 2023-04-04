using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.System.Languages;
using eShopSolution.ViewModels.Catalog.Products.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        //http://localhost:port/products?pageIndex=1&pageSize=10&Catego
        [HttpGet]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var languages = await _languageService.GetAll();
            return Ok(languages);
        }
    }
}
