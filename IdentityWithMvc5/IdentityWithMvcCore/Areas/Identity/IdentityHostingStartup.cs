using System;
using IdentityWithMvcCore.Areas.Identity.Data;
using IdentityWithMvcCore.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityWithMvcCore.Areas.Identity.IdentityHostingStartup))]
namespace IdentityWithMvcCore.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityCoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityCoreContextConnection")));

                services.AddDefaultIdentity<IdentityCoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityCoreContext>();
            });
        }
    }
}