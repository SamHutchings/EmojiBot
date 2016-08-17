using System;
using System.Linq;
using System.Web.Http;
using EmojiBot.Api.Models.Facebook.Inbound;
using EmojiBot.Core.Search;
using Ninject;

namespace EmojiBot.Api.Controllers
{
	[AllowAnonymous]
	public class MessageController : BaseApiController
	{
		[Inject]
		public IEmojiSearchService EmojiSearchService { get; set; }

		public IHttpActionResult Post([FromBody]WebhookModel model)
		{
			Log.InfoFormat("Webhook post: {0} entries", model.entry.Count());

			foreach (var entry in model.entry)
			{
				Log.InfoFormat("Entry with {0} events", entry.messaging.Count());

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
				Log.InfoFormat("Recieved from {0} with no content - ignoring", model.sender.id);

				return;
			}

			Log.InfoFormat("Recieved message {0} from {1}", model.message.text, model.sender.id);

			var inboundText = model.message.text.ToLower();

			if (inboundText.Contains("help "))
			{
				SendHelpMessage(model.sender.id);
			}
			else if (inboundText.Contains("hi ") || inboundText.Contains("hello "))
			{
				SendHelloMessage(model.sender.id);
			}
			else
			{
				SendEmojiSearchMessage(model.sender.id, inboundText);
			}
		}

		string GetName(string id)
		{
			var details = FacebookGraphService.GetUserDetails(id);

			if (details == null)
				return "";

			return String.Format(" {0}", details.first_name);
		}

		void SendHelpMessage(string id)
		{
			FacebookGraphService.SendMessage(id, String.Format("Hi{0}. To use this bot, please ask for an emoji you want. For example, send \"dragon emoji\".", GetName(id)));
		}

		void SendHelloMessage(string id)
		{
			FacebookGraphService.SendMessage(id, String.Format("Hi{0}. What emoji would you like?", GetName(id)));
		}

		void SendEmojiSearchMessage(string id, string text)
		{
			if (String.IsNullOrWhiteSpace(text))
			{
				FacebookGraphService.SendMessage(id, String.Format("We couldn't find an emoji that matches, sorry!", text));
				return;
			}

			var searchTerms = text.ToLower().Replace("please", "").Replace("emoji", "").Split(' ').Where(x => !String.IsNullOrWhiteSpace(x));

			if (!searchTerms.Any())
			{
				FacebookGraphService.SendMessage(id, String.Format("We couldn't find an emoji that matches, sorry!", text));
				return;
			}

			var results = EmojiSearchService.Search(searchTerms);

			if (!results.Any())
			{
				FacebookGraphService.SendMessage(id, String.Format("We couldn't find an emoji that matches, sorry!", text));
				return;
			}

			FacebookGraphService.SendMessage(id, String.Format("No problem! Here's the closest match for {0}:", String.Join(", ", searchTerms)));
			FacebookGraphService.SendMessage(id, results.First().Characters);
		}
	}
}