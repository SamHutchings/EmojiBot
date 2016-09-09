using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmojiBot.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Emoji",
				url: "{controller}/{action}/{characters}",
				defaults: new { controller = "Emoji", action = "Details" },
				constraints: new { controller = @"Emoji", action = @"Details", },
				namespaces: new[] { "EmojiBot.Web.Controllers" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "EmojiBot.Web.Controllers" }
			);
		}
	}
}
