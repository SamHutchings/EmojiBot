using EmojiBot.Api.Models.Facebook;
using EmojiBot.Api.Services;
using log4net;
using System;
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

		IFacebookGraphService _facebookService;

		public MessageController()
		{
			_facebookService = new FacebookGraphService();
		}

		public IHttpActionResult Post([FromBody]RecievedMessageModel model)
		{
			__log.InfoFormat("Recieved message {0} from {1}", model.message.text, model.sender.id);

			_facebookService.SendMessage(new SendMessageModel
			{
				recipient = new Models.Facebook.User { id = model.sender.id },
				message = new OutboundMessage
				{
					text = String.Format("You want emojis like {0}?", model.message.text)
				}
			});

			return Ok("Message Received");
		}
	}
}