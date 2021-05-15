using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

using eShopLegacyMVC.CustomMembership;
using eShopLegacyMVC.Models;
using eShopLegacyMVC.Models.Infrastructure;
using eShopLegacyMVC.Modules;

using log4net;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace eShopLegacyMVC
{
	public class MvcApplication : System.Web.HttpApplication
	{
		private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		IContainer container;

		protected void Application_Start()
		{
			container = RegisterContainer();
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			ConfigDataBase();
		}

		protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
		{
			HttpCookie authCookie = Request.Cookies["Cookie1"];
			if (authCookie != null)
			{
				FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

				var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

				CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

				principal.UserId = serializeModel.UserId;
				principal.FirstName = serializeModel.FirstName;
				principal.LastName = serializeModel.LastName;
				principal.Roles = serializeModel.RoleName.ToArray<string>();

				HttpContext.Current.User = principal;
			}
		}

		/// <summary>
		/// Track the machine name and the start time for the session inside the current session
		/// </summary>
		protected void Session_Start(Object sender, EventArgs e)
		{
			HttpContext.Current.Session["MachineName"] = Environment.MachineName;
			HttpContext.Current.Session["SessionStartTime"] = DateTime.Now;
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			//set the property to our new object
			LogicalThreadContext.Properties["activityid"] = new ActivityIdHelper();

			LogicalThreadContext.Properties["requestinfo"] = new WebRequestInfo();

			_log.Debug("WebApplication_BeginRequest");
		}

		/// <summary>
		/// http://docs.autofac.org/en/latest/integration/mvc.html
		/// </summary>
		protected IContainer RegisterContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
			var mockData = bool.Parse(ConfigurationManager.AppSettings["UseMockData"]);
			builder.RegisterModule(new ApplicationModule(mockData));

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
			return container;
		}

		private void ConfigDataBase()
		{
			var mockData = bool.Parse(ConfigurationManager.AppSettings["UseMockData"]);

			if (!mockData)
			{
				Database.SetInitializer<CatalogDBContext>(container.Resolve<CatalogDBInitializer>());
			}
		}

	}

	public class ActivityIdHelper
	{
		public override string ToString()
		{
			if (Trace.CorrelationManager.ActivityId == Guid.Empty)
			{
				Trace.CorrelationManager.ActivityId = Guid.NewGuid();
			}

			return Trace.CorrelationManager.ActivityId.ToString();
		}
	}

	public class WebRequestInfo
	{
		public override string ToString()
		{
			return HttpContext.Current?.Request?.RawUrl + ", " + HttpContext.Current?.Request?.UserAgent;
		}
	}
}
