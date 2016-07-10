using EmojiBot.Api.Models.Facebook.Inbound;
using EmojiBot.Api.Models.Facebook.Outbound;
using EmojiBot.Api.Services;
using log4net;
using System;
using System.Linq;
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

		public IHttpActionResult Post([FromBody]WebhookModel model)
		{
			__log.InfoFormat("Webhook post: {0} entries", model.entry.Count());

			foreach (var entry in model.entry)
			{
				__log.InfoFormat("Entry with {0} events", entry.messaging.Count());

				foreach (var message in entry.messaging)
				{
					ProcessReceivedMessage(message);
				}
			}

			return Ok();
		}

		void ProcessReceivedMessage(RecievedMessageModel model)
		{
			if (model.message == null || String.IsNullOrWhiteSpace(model.message.text))
			{
				__log.InfoFormat("Recieved from {0} with no content - ignoring", model.sender.id);

				return;
			}

			__log.InfoFormat("Recieved message {0} from {1}", model.message.text, model.sender.id);

			var outboundMessage = new SendMessageModel { recipient = new Models.Facebook.User { id = model.sender.id } };
			var inboundText = model.message.text.ToLower();

			if (inboundText.Contains("help"))
			{
				outboundMessage.message = HelpMessage(model.sender.id);
			}
			else if (inboundText.Contains("hi") || inboundText.Contains("hello"))
			{
				outboundMessage.message = HelloMessage(model.sender.id);
			}
			else
			{
				outboundMessage.message = EmojiSearch(inboundText);
			}

			_facebookService.SendMessage(outboundMessage);
		}

		string GetName(string id)
		{
			var details = _facebookService.GetUserDetails(id);

			if (details == null)
				return "";

			return String.Format(" {0}", details.first_name);
		}

		Models.Facebook.Outbound.Message HelpMessage(string id)
		{
			return new Models.Facebook.Outbound.Message { text = String.Format("Hi{0}. To use this bot, please ask for an emoji you want. For example, send \"dragon emoji\".", GetName(id)) };
		}

		Models.Facebook.Outbound.Message HelloMessage(string id)
		{
			return new Models.Facebook.Outbound.Message { text = String.Format("Hi{0}. What emoji would you like?", GetName(id)) };
		}

		Models.Facebook.Outbound.Message EmojiSearch(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return new Models.Facebook.Outbound.Message { text = String.Format("We couldn't find an emoji that matches, sorry!", text) };

			var searchTerm = text.ToLower().Replace("please", "").Replace("emoji", "");

			if (String.IsNullOrWhiteSpace(searchTerm))
				return new Models.Facebook.Outbound.Message { text = String.Format("We couldn't find an emoji that matches, sorry!", searchTerm) };

			if (searchTerm.Contains("dragon"))
			{
				return new Models.Facebook.Outbound.Message { text = String.Format("Here's your emoji: 🐉. Thanks for using emojibot!", searchTerm) };
			}
			else if (searchTerm.Contains("ghost"))
			{
				return new Models.Facebook.Outbound.Message { text = String.Format("Here's your emoji: 👻. Thanks for using emojibot!", searchTerm) };
			}
			else if (searchTerm.Contains("100") || searchTerm.Contains("hundred"))
			{
				return new Models.Facebook.Outbound.Message { text = String.Format("Here's your emoji: 💯. Thanks for using emojibot!", searchTerm) };
			}
			else if (searchTerm.Contains("smug"))
			{
				return new Models.Facebook.Outbound.Message { text = String.Format("Here's your emoji: 🤔. Thanks for using emojibot!", searchTerm) };
			}
			else if (searchTerm.Contains("poo"))
			{
				return new Models.Facebook.Outbound.Message { text = String.Format("Here's your emoji: 💩. Thanks for using emojibot!", searchTerm) };
			}

			return new Models.Facebook.Outbound.Message { text = String.Format("We couldn't find an emoji that matches {0}, sorry!", searchTerm) };
		}
	}
}