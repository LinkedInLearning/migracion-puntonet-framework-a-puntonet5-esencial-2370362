using eShop.Data;

using eShopMVC.Services;
using eShopMVC.ViewModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class CatalogController : ControllerBase
	{
		private readonly ICatalogService _catalogService;

		public CatalogController(ICatalogService catalogService)
		{
			_catalogService = catalogService;
		}

		[HttpGet]
		public ActionResult<PaginatedItemsViewModel<CatalogItem>> GetItems(int pageSize = 10, int pageIndex = 1)
		{
			return _catalogService.GetCatalogItemsPaginated(pageSize, pageIndex);
		}

		[HttpPost]
		public IActionResult Create([Bind(include: "Id,Name,Description,Price,PictureFileName,CatalogTypeId,CatalogBrandId,AvailableStock,RestockThreshold,MaxStockThreshold,OnReorder")] CatalogItem catalogItem)
		{
			_catalogService.CreateCatalogItem(catalogItem);
			return Created($"/api/catalog/{catalogItem.Id}", catalogItem);
		}
	}
}
