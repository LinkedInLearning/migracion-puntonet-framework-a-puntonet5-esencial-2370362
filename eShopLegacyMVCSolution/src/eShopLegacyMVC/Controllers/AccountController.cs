using eShopLegacyMVC.CustomMembership;
using eShopLegacyMVC.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eShopLegacyMVC.Controllers.Account
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		// GET: Account
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Login(string ReturnUrl = "")
		{
			if (User.Identity.IsAuthenticated)
			{
				return LogOut();
			}
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginView loginView, string ReturnUrl = "")
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(loginView.UserName, loginView.Password))
				{
					var user = (CustomMembershipUser)Membership.GetUser(loginView.UserName, false);
					if (user != null)
					{
						CustomSerializeModel userModel = new Models.CustomSerializeModel()
						{
							FirstName = user.FirstName,
							LastName = user.LastName,
							RoleName = user.Roles.Select(r => r.RoleName).ToList()
						};

						string userData = JsonConvert.SerializeObject(userModel);
						FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
							(
							1, loginView.UserName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
							);

						string enTicket = FormsAuthentication.Encrypt(authTicket);
						HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
						Response.Cookies.Add(faCookie);
					}

					if (Url.IsLocalUrl(ReturnUrl))
					{
						return Redirect(ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index");
					}
				}
			}
			ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
			return View(loginView);
		}

		[HttpGet]
		public ActionResult Registration()
		{
			return View();
		}

		[HttpPost]

		public ActionResult LogOut()
		{
			HttpCookie cookie = new HttpCookie("Cookie1", "");
			cookie.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(cookie);

			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account", null);
		}
	}
}
