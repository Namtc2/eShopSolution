using eShopSolution.AdminApp.Services;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var langId = HttpContext.Session.GetString(SystemConstants.AppSetting.DefaultLanguageId);
            var request = new GetManageProductPagingRequest()
            {
                KeyWord = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = langId
            };
            var data = await _productApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.Data);
        }
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Consumes("multipart/form-data")] 
        
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);
            var result = await _productApiClient.CreateProduct(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
    }
}
