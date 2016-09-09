namespace EmojiBot.Web.Controllers
{
	using EmojiBot.Core.Domain;
	using NHibernate.Linq;
	using System.Linq;
	using System.Web.Mvc;

	public class EmojiController : BaseController
	{
		public ActionResult Details(string characters)
		{
			var emoji = DatabaseSession.Query<Emoji>()
				.Where(x => x.Characters == characters)
				.FirstOrDefault();

			if (emoji == null)
				return HttpNotFound();

			return View(emoji);
		}
	}
}