using eShopMVC.Services;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.IO;

namespace eShopMVC.Controllers
{
	public class PicController : Controller
	{
		private readonly ILogger<PicController> _logger;
		private readonly ICatalogService _service;

		private readonly IWebHostEnvironment _hostingEnvironment;

		public const string GetPicRouteName = "GetPicRouteTemplate";


		public PicController(ILogger<PicController> logger, ICatalogService service, IWebHostEnvironment hostingEnvironment)
		{
			_logger = logger;
			_service = service;
			_hostingEnvironment = hostingEnvironment;
		}

		// GET: Pic/5.png
		[HttpGet("items/{catalogItemId:int}/pic", Name = GetPicRouteName)]
		public IActionResult Index(int catalogItemId)
		{
			_logger.LogInformation($"Now loading... /items/Index?{catalogItemId}/pic");

			if (catalogItemId <= 0)
			{
				return BadRequest();
			}

			var item = _service.FindCatalogItem(catalogItemId);

			if (item != null)
			{
				var path = Path.Combine(_hostingEnvironment.WebRootPath, "Pics", item.PictureFileName);

				string imageFileExtension = Path.GetExtension(item.PictureFileName);
				string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

				var buffer = System.IO.File.ReadAllBytes(path);

				return File(buffer, mimetype);
			}

			return NotFound();
		}

		private string GetImageMimeTypeFromImageFileExtension(string extension)
		{
			string mimetype;

			switch (extension)
			{
				case ".png":
					mimetype = "image/png";
					break;
				case ".gif":
					mimetype = "image/gif";
					break;
				case ".jpg":
				case ".jpeg":
					mimetype = "image/jpeg";
					break;
				case ".bmp":
					mimetype = "image/bmp";
					break;
				case ".tiff":
					mimetype = "image/tiff";
					break;
				case ".wmf":
					mimetype = "image/wmf";
					break;
				case ".jp2":
					mimetype = "image/jp2";
					break;
				case ".svg":
					mimetype = "image/svg+xml";
					break;
				default:
					mimetype = "application/octet-stream";
					break;
			}

			return mimetype;
		}
	}
}
