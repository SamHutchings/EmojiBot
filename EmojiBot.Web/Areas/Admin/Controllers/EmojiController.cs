using EmojiBot.Core.Domain;
using EmojiBot.Core.Search;
using EmojiBot.Web.Areas.Admin.Models;
using NHibernate.Linq;
using Ninject;
using System.Linq;
using System.Web.Mvc;

namespace EmojiBot.Web.Areas.Admin.Controllers
{
	public class EmojiController : BaseAdminController
	{
		[Inject]
		public IEmojiSearchService EmojiSearchService { get; set; }

		public ActionResult Index()
		{
			return View(DatabaseSession.Query<Emoji>());
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var emoji = DatabaseSession.Get<Emoji>(id);

			if (emoji == null)
				return HttpNotFound();

			var model = new EmojiEditModel { Characters = emoji.Characters, Description = emoji.Description, Keywords = emoji.Keywords, Name = emoji.Name };

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(int id, EmojiEditModel model)
		{
			var emoji = DatabaseSession.Get<Emoji>(id);

			if (emoji == null)
				return HttpNotFound();

			emoji.Name = model.Name;
			emoji.Description = model.Description;
			emoji.Keywords = model.Keywords;

			EmojiSearchService.Index(emoji);

			return View(model);
		}

		public ActionResult IndexAll()
		{
			var allEmojis = DatabaseSession.Query<Emoji>()
				.ToList();

			EmojiSearchService.Index(allEmojis);

			return Content("Indexed!!!!");
		}
	}
}