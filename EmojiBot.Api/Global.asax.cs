using System.Web.Http;
using System.Web.Mvc;

namespace EmojiBot
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			log4net.Config.XmlConfigurator.Configure();
		}
	}
}
