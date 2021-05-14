using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC.Config
{
	public class PaginationConfig
	{
		public static readonly string ConfigSection = "Pagination";

		public int PageSize { get; set; }
	}
}
