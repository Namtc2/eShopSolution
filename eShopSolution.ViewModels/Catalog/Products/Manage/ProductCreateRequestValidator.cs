using eShopSolution.ViewModels.System.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products.Manage
{
    public class ProductCreateRequestValidator:AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(x => x.SeoTitle).NotEmpty().WithMessage("Trường này cần được điền");
            RuleFor(x=>x.Details).NotEmpty().WithMessage("Trường này cần được điền").MinimumLength(5).WithMessage("Tối thiểu 5 ký tự").MaximumLength(500).WithMessage("Tối đa 500 ký tự");
        }
    }   
}
