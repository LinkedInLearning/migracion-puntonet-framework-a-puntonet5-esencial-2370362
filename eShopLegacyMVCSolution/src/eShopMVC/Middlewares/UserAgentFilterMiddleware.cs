using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eShopMVC.Middlewares
{
	public class UserAgentFilterMiddleware
	{
		private readonly RequestDelegate _next;

		public UserAgentFilterMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var isEdge = context.Request.Headers.Where(h => h.Key == "User-Agent").Any(h => h.Value.Any(v => v.Contains("Edg/")));
			if (!isEdge)
			{
				context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				await context.Response.WriteAsync("<html><head><meta charset=\"UTF-8\" /><title>Página no compatible</title></head><body><p>Lo sentimos, pero esta página solo es compatible con edge.</p></body></html>");
			}
			else
			{
				// Llamamos al siguiente delegado en la cadena de responsabilidad de middlewares:
				await _next(context);
			}
		}
	}
}