using EmojiBot.Api.Services;
using log4net;
using NHibernate;
using Ninject;
using System.Web.Http;

namespace EmojiBot.Api.Controllers
{
	public class BaseApiController : ApiController
	{
		[Inject]
		public IFacebookGraphService FacebookGraphService { get; set; }

		[Inject]
		public ISession Session { get; set; }

		[Inject]
		public ILog Log { get; set; }
	}
}