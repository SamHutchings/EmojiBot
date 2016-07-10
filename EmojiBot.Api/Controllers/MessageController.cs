using log4net;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace EmojiBot.Api.Controllers
{
	[AllowAnonymous]
	public class MessageController : BaseApiController
	{
		static readonly ILog __log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		string _verificationCode;

		public MessageController()
		{
			_verificationCode = ConfigurationManager.AppSettings["facebook.verification-code"];
		}

		public HttpResponseMessage Get()
		{
			var verifyToken = HttpContext.Current.Request.QueryString["hub.verify_token"];
			var challenge = HttpContext.Current.Request.QueryString["hub.challenge"];

			__log.InfoFormat("Request from facebook received: verify {0}, challenge {1}", verifyToken, challenge);

			if (verifyToken != _verificationCode)
			{
				return new HttpResponseMessage(HttpStatusCode.BadRequest);
			}

			var response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StringContent(challenge);
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
			return response;
		}

		public IHttpActionResult Post([FromBody]string message)
		{
			return Ok("Message Received");
		}
	}
}