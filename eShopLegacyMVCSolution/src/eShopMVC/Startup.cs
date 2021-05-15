using eShop.Data;

using eShopMVC.Config;
using eShopMVC.Extensions;
using eShopMVC.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<CatalogDBContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddControllersWithViews();
			var useMockData = Configuration.GetValue<bool>("UseMockData");
			if (useMockData)
			{
				services.AddScoped<ICatalogService, CatalogServiceMock>();
			}
			else
			{
				services.AddScoped<ICatalogService, CatalogService>();
			}
			services.AddSingleton<IUserService, UserService>();
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.ReturnUrlParameter = "returnUrl";
					options.LoginPath = "/login";
    });
			services.Configure<PaginationConfig>(Configuration.GetSection(PaginationConfig.ConfigSection));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			// app.UseUserAgentFilter();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
						name: "arias",
						pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute(
						name: "default",
						pattern: "{controller=catalog}/{action=Index}/{id?}");
			});
		}
	}
}
