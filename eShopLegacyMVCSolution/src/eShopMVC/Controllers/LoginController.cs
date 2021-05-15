using eShopMVC.Services;
using eShopMVC.ViewModel;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eShopMVC.Controllers
{
	public class LoginController : Controller
	{

		private readonly IUserService _userService;

		public LoginController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Index([FromQuery]string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index([FromQuery]string returnUrl, [FromForm] LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = _userService.GetUserByUsername(model.Username);
			if (user is null)
			{
				ModelState.AddModelError("Username", "The username does not exists.");
				return View(model);
			}
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim("FullName", $"{user.FirstName} {user.LastName}"),
				new Claim(ClaimTypes.Role, "Administrator"),
			};

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				AllowRefresh = true,
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				IsPersistent = true,
				IssuedUtc = DateTimeOffset.UtcNow,
				RedirectUri = this.Url.RouteUrl("default", new { Controller = "Login" }, this.Request.Scheme),
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
			if (string.IsNullOrWhiteSpace(returnUrl))
			{
				returnUrl = Url.RouteUrl("default", new { Controller = "Catalog", Action = "Index" }, Request.Scheme);
			}
			return Redirect(returnUrl);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Catalog");
		}
	}
}