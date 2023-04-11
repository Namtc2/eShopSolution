using eShopSolution.WebApp.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _sharedCultureLocalizer;

        public HomeController(ILogger<HomeController> logger, ISharedCultureLocalizer sharedCultureLocalizer)
        {
            _logger = logger;
            _sharedCultureLocalizer = sharedCultureLocalizer;
        }

        public IActionResult Index()
        {
            var msg = _sharedCultureLocalizer.GetLocalizedString("Vietnamese");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Privacy1()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
		public IActionResult SetCultureCookie(string cltr, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
				);

			return LocalRedirect(returnUrl);
		}
	}
}
