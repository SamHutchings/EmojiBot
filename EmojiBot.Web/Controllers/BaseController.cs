using NHibernate;
using Ninject;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	public class BaseController : Controller
	{
		[Inject]
		public ISession DatabaseSession { get; set; }
	}
}