using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC.ViewModel
{
	public class LoginViewModel
	{

		[Required(ErrorMessage = "The username is mandatory.")]
		public string Username { get; set; }

		[Required(ErrorMessage = "The password is mandatory.")]
		public string Password { get; set; }

	}
}
