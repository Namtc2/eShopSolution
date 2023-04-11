﻿using eShopSolution.Application.Common;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using eShopSolution.ViewModels.System.Roles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace eShopSolution.ApiIntegration
{
	public class LanguageApiClient : BaseApiClient, ILanguageApiClient
	{
		public LanguageApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
			: base(httpClientFactory, configuration, httpContextAccessor)
		{
		}
		public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
		{
			return await GetAsync<ApiResult<List<LanguageViewModel>>>("/api/languages");
		}
	}
}
