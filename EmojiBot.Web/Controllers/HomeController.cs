using EmojiBot.Core.Domain;
using NHibernate.Linq;
using System.Linq;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			var emojis = DatabaseSession.Query<Emoji>()
				.ToList()
				.GroupBy(x => x.Category)
				.OrderBy(x => x.Key == null ? int.MaxValue : x.Key.SortOrder);

			return View(emojis);
		}

		public ActionResult TopBar()
		{
			if (AuthenticatedUser == null)
				return new EmptyResult();

			return PartialView();
		}
	}
}