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

		public ActionResult TopBar()
		{
			if (AuthenticatedUser == null)
				return new EmptyResult();

			return PartialView();
		}
	}
}