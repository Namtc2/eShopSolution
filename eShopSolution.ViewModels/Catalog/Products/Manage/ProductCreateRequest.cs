using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products.Manage
{
    public class ProductCreateRequest
    {
        //for table product
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }

        //for table productTranslate
        [Required(ErrorMessage ="Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        [Required(ErrorMessage ="Bạn phải chọn ảnh sản phẩm")]
        public IFormFile ThumbnailImage { set; get; }
    }
}
