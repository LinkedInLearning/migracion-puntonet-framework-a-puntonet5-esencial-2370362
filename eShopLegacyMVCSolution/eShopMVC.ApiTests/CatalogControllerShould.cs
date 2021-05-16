using AngleSharp.Html.Dom;

using eShop.Data;

using eShopMVC.ApiTests.Helpers;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

namespace eShopMVC.ApiTests
{
	public class CatalogControllerShould :
		IClassFixture<CustomWebApplicationFactory<eShopMVC.Startup>>
	{
		private readonly HttpClient _client;
		private readonly CustomWebApplicationFactory<eShopMVC.Startup>
			_factory;

		public CatalogControllerShould(
			CustomWebApplicationFactory<eShopMVC.Startup> factory)
		{
			_factory = factory;
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}

		[Fact]
		public async Task AllActionLinks_Return_StatusCodeOK()
		{
			var defaultPage = await _client.GetAsync("/");
			Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);

			var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
			var actionLinks = content.QuerySelectorAll(".esh-table-link");
			Assert.True(actionLinks.Count() > 0);
			foreach (var actionLink in actionLinks)
			{
				var anchor = actionLink as IHtmlAnchorElement;
				var url = anchor.Href;
				var editResponse = await _client.GetAsync(url);
				content = await HtmlHelpers.GetDocumentAsync(editResponse);
				Assert.Equal(HttpStatusCode.OK, editResponse.StatusCode);
			}
		}
	}
}
