using eShopMVC.Middlewares;

using Microsoft.AspNetCore.Builder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopMVC.Extensions
{
	public static class UserAgentFilterMIddlewareExtensions
	{

		public static IApplicationBuilder UseUserAgentFilter(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<UserAgentFilterMiddleware>();

	}
}
