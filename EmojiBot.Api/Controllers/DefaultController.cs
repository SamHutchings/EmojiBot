using System.Web.Http;

namespace EmojiBot.Api.Controllers
{
	public class DefaultController : ApiController
	{
		public IHttpActionResult Get()
		{
			return Ok();
		}
	}
}
