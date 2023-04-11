using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Common;

namespace eShopSolution.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext _dbContext;
        public CategoryService(EShopDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResult<List<CategoryViewModel>>> GetAll(string languageId)
        {
            var query = from c in _dbContext.Categories
                        join ct in _dbContext.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new CategoryViewModel
                        {
                            Id = c.Id,
                            Name = ct.Name
                        };
           var data =  await query.ToListAsync();
           return new ApiSuccessResult<List<CategoryViewModel>>(data);
        }
    }
}
