using eShopSolution.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiSuccessResult<T>: ApiResult<T>
    {
        public ApiSuccessResult(T data) 
        {
            IsSuccessed = true;
            Data = data;        
        }
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
