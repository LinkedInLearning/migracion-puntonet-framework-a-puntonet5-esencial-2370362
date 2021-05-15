using eShop.Data;

using eShopMVC.Config;
using eShopMVC.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace eShopMVC.Controllers
{
	public class CatalogController : Controller
	{
		private readonly ILogger<CatalogController> _logger;

		private readonly ICatalogService _service;

		private readonly ICountryService _countryService;

		private readonly PaginationConfig _paginationConfig;

		public CatalogController(ILogger<CatalogController> logger, ICatalogService service, IOptions<PaginationConfig> paginationConfig, ICountryService countryService)
		{
			_logger = logger;
			_service = service;
			_paginationConfig = paginationConfig.Value;
			_countryService = countryService;
		}

		// GET /[?pageSize=3&pageIndex=10]
		public async Task<IActionResult> Index(int? pageSize, int pageIndex = 0)
		{
			pageSize ??= _paginationConfig.PageSize;

			_logger.LogInformation($"Now loading... /Catalog/Index?pageSize={pageSize}&pageIndex={pageIndex}");
			var country = await _countryService.GetCountryByIpAddress(HttpContext.Connection.RemoteIpAddress.ToString());
			var paginatedItems = _service.GetCatalogItemsPaginated(pageSize.Value, pageIndex, country);
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

		// GET: Catalog/Create
		public ActionResult Create()
		{
			_logger.LogInformation($"Now loading... /Catalog/Create");
			ViewBag.CatalogBrandId = new SelectList(_service.GetCatalogBrands(), "Id", "Brand");
			ViewBag.CatalogTypeId = new SelectList(_service.GetCatalogTypes(), "Id", "Type");
			return View(new CatalogItem());
		}

		// POST: Catalog/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind("Id,Name,Description,Price,PictureFileName,CatalogTypeId,CatalogBrandId,AvailableStock,RestockThreshold,MaxStockThreshold,OnReorder")] CatalogItem catalogItem)
		{
			_logger.LogInformation($"Now processing... /Catalog/Create?catalogItemName={catalogItem.Name}");
			if (ModelState.IsValid)
			{
				_service.CreateCatalogItem(catalogItem);
				return RedirectToAction("Index");
			}

			ViewBag.CatalogBrandId = new SelectList(_service.GetCatalogBrands(), "Id", "Brand", catalogItem.CatalogBrandId);
			ViewBag.CatalogTypeId = new SelectList(_service.GetCatalogTypes(), "Id", "Type", catalogItem.CatalogTypeId);
			return View(catalogItem);
		}

		// GET: Catalog/Edit/5
		public ActionResult Edit(int? id)
		{
			_logger.LogInformation($"Now loading... /Catalog/Edit?id={id}");
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
			ViewBag.CatalogBrandId = new SelectList(_service.GetCatalogBrands(), "Id", "Brand", catalogItem.CatalogBrandId);
			ViewBag.CatalogTypeId = new SelectList(_service.GetCatalogTypes(), "Id", "Type", catalogItem.CatalogTypeId);
			return View(catalogItem);
		}

		// POST: Catalog/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind("Id,Name,Description,Price,PictureFileName,CatalogTypeId,CatalogBrandId,AvailableStock,RestockThreshold,MaxStockThreshold,OnReorder")] CatalogItem catalogItem)
		{
			_logger.LogInformation($"Now processing... /Catalog/Edit?id={catalogItem.Id}");
			if (ModelState.IsValid)
			{
				_service.UpdateCatalogItem(catalogItem);
				return RedirectToAction("Index");
			}
			ViewBag.CatalogBrandId = new SelectList(_service.GetCatalogBrands(), "Id", "Brand", catalogItem.CatalogBrandId);
			ViewBag.CatalogTypeId = new SelectList(_service.GetCatalogTypes(), "Id", "Type", catalogItem.CatalogTypeId);
			return View(catalogItem);
		}

		// GET: Catalog/Delete/5
		public ActionResult Delete(int? id)
		{
			_logger.LogInformation($"Now loading... /Catalog/Delete?id={id}");
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

		// POST: Catalog/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			_logger.LogInformation($"Now processing... /Catalog/DeleteConfirmed?id={id}");
			CatalogItem catalogItem = _service.FindCatalogItem(id);
			_service.RemoveCatalogItem(catalogItem);
			return RedirectToAction("Index");
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
