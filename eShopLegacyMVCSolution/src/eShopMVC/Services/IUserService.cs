using eShopMVC.Models;

namespace eShopMVC.Services
{
	public interface IUserService
	{
		User GetUserByUsername(string username);
	}
}