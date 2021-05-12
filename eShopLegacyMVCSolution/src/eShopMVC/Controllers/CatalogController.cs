using eShop.Data;

using eShopMVC.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net;

namespace eShopMVC.Controllers
{
	public class CatalogController : Controller
	{
		private readonly ILogger<CatalogController> _logger;

		private ICatalogService _service;

		public CatalogController(ILogger<CatalogController> logger, ICatalogService service)
		{
			_logger = logger;
			_service = service;
		}

		// GET /[?pageSize=3&pageIndex=10]
		public IActionResult Index(int pageSize = 10, int pageIndex = 0)
		{
			_logger.LogInformation($"Now loading... /Catalog/Index?pageSize={pageSize}&pageIndex={pageIndex}");
			var paginatedItems = _service.GetCatalogItemsPaginated(pageSize, pageIndex);
			ChangeUriPlaceholder(paginatedItems.Data);
			return View(paginatedItems);
		}

		// GET: Catalog/Details/5
		public ActionResult Details(int? id)
		{
			_logger.LogInformation($"Now loading... /Catalog/Details?id={id}");
			if (id == null)
			{
				return StatusCode((int)HttpStatusCode.BadRequest);
			}
			CatalogItem catalogItem = _service.FindCatalogItem(id.Value);
			if (catalogItem == null)
			{
				return NotFound();
			}
			AddUriPlaceHolder(catalogItem);

			return View(catalogItem);
		}



		protected override void Dispose(bool disposing)
		{
			_logger.LogInformation($"Now disposing");
			if (disposing)
			{
				_service.Dispose();
			}
			base.Dispose(disposing);
		}

		private void ChangeUriPlaceholder(IEnumerable<CatalogItem> items)
		{
			foreach (var catalogItem in items)
			{
				AddUriPlaceHolder(catalogItem);
			}
		}

		private void AddUriPlaceHolder(CatalogItem item)
		{
			item.PictureUri = this.Url.RouteUrl(PicController.GetPicRouteName, new { catalogItemId = item.Id }, this.Request.Scheme);
		}
	}
}
