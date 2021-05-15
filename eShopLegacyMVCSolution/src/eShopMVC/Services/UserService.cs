using eShopMVC.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC.Services
{
	public class UserService : IUserService
	{

		private readonly List<User> _users = new List<User>
		{
			new User { Email = "juanjo@fakemail.com", FirstName = "Juanjo", LastName = "Montiel", Username = "juanjo", Password = "Pass123456" }
		};

		public User GetUserByUsername(string username) =>
			_users.SingleOrDefault(u => u.Username == username);
	}
}
