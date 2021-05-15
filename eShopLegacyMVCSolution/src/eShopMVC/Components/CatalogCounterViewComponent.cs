using eShopMVC.Services;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC.Components
{
	public class CatalogCounterViewComponent : ViewComponent
	{

		private readonly ICatalogService _catalogService;
		
		public CatalogCounterViewComponent(ICatalogService catalogService)
		{
			_catalogService = catalogService;
		}

		public Task<IViewComponentResult> InvokeAsync()
		{
			return Task.FromResult<IViewComponentResult>(View(_catalogService.GetItemsCount()));
		}

	}
}
