using EmojiBot.Web.Models;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	public class SearchController : BaseController
	{
		public ActionResult Header()
		{
			return PartialView();
		}

		[HttpPost]
		public ActionResult Index(EmojiSearchModel model)
		{
			var results = EmojiSearchService.Search(model.Term);

			ViewBag.Term = model.Term;

			return View(results);
		}
	}
}