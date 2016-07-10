using System.Collections.Generic;

namespace EmojiBot.Api.Models.Facebook.Inbound
{
	public class WebhookModel
	{
		public string @object { get; set; }

		public IEnumerable<Entry> entry { get; set; }

		public WebhookModel()
		{
			entry = new List<Entry>();
		}
	}
}