using eShopSolution.ApiIntegration;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
	public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryApiClient _categoryApiClient;
        public ProductController(IProductApiClient productApiClient, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10, int categoryId = 0)
        {
            var langId = HttpContext.Session.GetString(SystemConstants.AppSetting.DefaultLanguageId);
            var request = new GetManageProductPagingRequest()
            {
                KeyWord = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = langId,
                CategoryId = categoryId
            };
            var data = await _productApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            var categories =await _categoryApiClient.GetAll(langId);
            if (categories.IsSuccessed)
            {
                ViewBag.Categories = categories.Data.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = categoryId == x.Id?true:false
                });
            }
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
        [HttpPost]
        public ActionResult UploadImage(List<IFormFile> files)
        {
            var filePath = "";
            foreach (var file in Request.Form.Files)
            {
                string serverMapPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image",file.FileName);
                using (var stream = new FileStream(serverMapPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                filePath = Url.Content($"~/Image/{file.FileName}");
            }
            return Json(new { url = filePath });
        }
    }
}
