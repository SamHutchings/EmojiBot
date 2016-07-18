﻿using EmojiBot.Core.Domain;
using EmojiBot.Web.Areas.Admin.Controllers;
using EmojiBot.Web.Areas.Admin.Models;
using NHibernate.Linq;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	public class EmojiController : BaseAdminController
	{
		public ActionResult Index()
		{
			return View(DatabaseSession.Query<Emoji>());
		}

		public ActionResult Edit(int id)
		{
			var emoji = DatabaseSession.Get<Emoji>(id);

			if (emoji == null)
				return HttpNotFound();

			var model = new EmojiEditModel { Characters = emoji.Characters, Description = emoji.Description, Keywords = emoji.Keywords, Name = emoji.Name };

			return View(model);
		}

		public ActionResult Edit(int id, EmojiEditModel model)
		{
			var emoji = DatabaseSession.Get<Emoji>(id);

			if (emoji == null)
				return HttpNotFound();

			emoji.Name = model.Name;
			emoji.Description = model.Description;
			emoji.Keywords = model.Keywords;

			return View(model);
		}
	}
}