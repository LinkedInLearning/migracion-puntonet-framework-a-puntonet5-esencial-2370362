using eShop.Data;

using eShopMVC.ViewModel;

using System;
using System.Collections.Generic;

namespace eShopMVC.Services
{
	public interface ICatalogService : IDisposable
	{
		CatalogItem FindCatalogItem(int id);
		IEnumerable<CatalogBrand> GetCatalogBrands();
		PaginatedItemsViewModel<CatalogItem> GetCatalogItemsPaginated(int pageSize, int pageIndex, string country = null);
		IEnumerable<CatalogType> GetCatalogTypes();
		void CreateCatalogItem(CatalogItem catalogItem);
		void UpdateCatalogItem(CatalogItem catalogItem);
		void RemoveCatalogItem(CatalogItem catalogItem);
	}
}