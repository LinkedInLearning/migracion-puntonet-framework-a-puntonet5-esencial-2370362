using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC.Services
{
	public class CountryService : ICountryService
	{

		public Task<string> GetCountryByIpAddress(string ipAddress) => Task.FromResult("Spain");
	}
}
