using EmojiBot.Core.Domain;
using EmojiBot.Core.Search;
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

		[Inject]
		public IEmojiSearchService EmojiSearchService { get; set; }

		public User AuthenticatedUser
		{
			get { return AuthenticationProvider.GetAuthenticatedUser(); }
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!filterContext.IsChildAction)
				DatabaseSession.BeginTransaction();
		}

		protected override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			if (!filterContext.IsChildAction)
			{
				DatabaseSession.Transaction.Commit();
				DatabaseSession.Dispose();
			}
		}
	}
}