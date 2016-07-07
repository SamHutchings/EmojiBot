using System.Web.Http;

namespace EmojiBot.Api.Controllers
{
	[AllowAnonymous]
	public class MessageController : ApiController
	{
		public IHttpActionResult Get()
		{
			return Ok();
		}

		public IHttpActionResult Post([FromBody]string message)
		{
			return Ok();
		}
	}
}