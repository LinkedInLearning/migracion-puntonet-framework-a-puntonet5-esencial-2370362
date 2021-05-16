using System.Collections.Generic;

using eShop.Data;


namespace eShopMVC.ApiTests
{
	public static class Utilities
	{

		public static void ReinitializeDbForTests(CatalogDBContext db)
		{
			db.Database.EnsureDeleted();
			db.Database.EnsureCreated();
		}
	}
}
