using eShopSolution.Application.Common;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class ProductApiClient :BaseApiClient,IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            :base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<ProductViewModel>> CreateProduct(ProductCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSetting.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.Token));
            var requestContent = new MultipartFormDataContent();
            var languageId = _contextAccessor.HttpContext.Session.GetString(SystemConstants.AppSetting.DefaultLanguageId);
            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(languageId), "languageId");
            var listProperties =  request.GetType().GetProperties();
            foreach (var property in listProperties)
            {
                if (property.Name.ToLower() == "thumbnailImage" || property.Name.ToLower() == "languageId")
                    continue;               
                requestContent.Add(property.GetValue(request)==null? new StringContent(""):new StringContent(property.GetValue(request).ToString()), property.Name.ToLower());
            }
            var response = await client.PostAsync($"/api/products/", requestContent);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ApiResult<ProductViewModel> data = (ApiResult<ProductViewModel>)JsonConvert.DeserializeObject(body, typeof(ApiResult<ProductViewModel>));
                return data;
            }
            else
                return JsonConvert.DeserializeObject<ApiResult<ProductViewModel>>(body);
        }

        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetPagings(GetManageProductPagingRequest request)
        {
           var data = await base.GetAsync<ApiResult<PagedResult<ProductViewModel>>>($"/api/products/paging?pageIndex={request.PageIndex}" +
                                                                                    $"&pageSize={request.PageSize}" +
                                                                                    $"&keyword={request.KeyWord}" +
                                                                                    $"&languageId={request.LanguageId}");
            return data;
        }
    }
}
