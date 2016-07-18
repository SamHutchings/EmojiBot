using EmojiBot.Core.Domain;
using EmojiBot.Web.Infrastructure;
using NHibernate;
using Ninject;
using System.Web.Mvc;

namespace EmojiBot.Web.Controllers
{
	public class BaseController : Controller
	{
		[Inject]
		public ISession DatabaseSession { get; set; }

		[Inject]
		public IAuthenticationProvider AuthenticationProvider { get; set; }

		public User AuthenticatedUser
		{
			get { return AuthenticationProvider.GetAuthenticatedUser(); }
		}
	}
}