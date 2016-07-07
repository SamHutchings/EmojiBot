using System.Net.Http.Headers;
using System.Web.Http;

namespace EmojiBot
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "{controller}/{id}/{action}",
				defaults: new { controller = "Default", id = RouteParameter.Optional, action = RouteParameter.Optional }
			);
		}
	}
}
