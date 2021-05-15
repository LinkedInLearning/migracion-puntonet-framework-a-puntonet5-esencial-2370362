using eShopLegacyMVC.Models;
using eShopLegacyMVC.Services;
using eShopLegacyMVC.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShopLegacyMVC.Api
{
	public class CatalogController : ApiController
	{
		private readonly ICatalogService _catalogService;

		public CatalogController(ICatalogService catalogService)
		{
			_catalogService = catalogService;
		}

		[HttpGet]
		public PaginatedItemsViewModel<CatalogItem> GetItems(int pageSize = 10, int pageIndex = 1)
		{
			return _catalogService.GetCatalogItemsPaginated(pageSize, pageIndex);
		}

		public IHttpActionResult Create(CatalogItem catalogItem)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_catalogService.CreateCatalogItem(catalogItem);
			return Created($"/api/catalog/{catalogItem.Id}", catalogItem);
		}

	}
}
