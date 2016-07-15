using EmojiBot.Core.Domain;
using NHibernate.Linq;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			return View(DatabaseSession.Query<Emoji>());
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}