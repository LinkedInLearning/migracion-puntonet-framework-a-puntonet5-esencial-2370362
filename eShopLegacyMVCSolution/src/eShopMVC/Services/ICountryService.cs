using System.Threading.Tasks;

namespace eShopMVC.Services
{
	public interface ICountryService
	{
		Task<string> GetCountryByIpAddress(string ipAddress);
	}
}