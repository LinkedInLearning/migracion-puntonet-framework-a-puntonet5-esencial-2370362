using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace eShopLegacyMVC.CustomMembership
{
	public class CustomMembershipUser : MembershipUser
	{
		#region User Properties  
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Password { get; set; }
		
		private string _username;
		public override string UserName => _username;

		public ICollection<Role> Roles { get; set; }

		#endregion

		public CustomMembershipUser(string username, string email, string password, string firstName, string lastName, IEnumerable<string> roles)
			: base()
		{
			_username = username;
			Password = password;
			FirstName = firstName;
			LastName = lastName;
			Roles = roles.Select(r => new Role { RoleName = r }).ToList();
		}
	}
}