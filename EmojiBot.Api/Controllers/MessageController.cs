using log4net;
using System.Web;
using System.Web.Http;

namespace EmojiBot.Api.Controllers
{
	[AllowAnonymous]
	public class MessageController : BaseApiController
	{
		static readonly ILog __log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public IHttpActionResult Get()
		{
			var verifyToken = HttpContext.Current.Request.QueryString["hub.verify"];
			var challenge = HttpContext.Current.Request.QueryString["hub.challenge"];

			__log.InfoFormat("Request from facebook received: verify {0}, challenge {1}", verifyToken, challenge);

			if (verifyToken == "")
			{
				return BadRequest("Verify token incorrect");
			}

			return Ok(challenge);
		}

		public IHttpActionResult Post([FromBody]string message)
		{
			return Ok("Message Received");
		}
	}
}