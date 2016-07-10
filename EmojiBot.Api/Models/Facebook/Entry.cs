using System.Collections.Generic;

namespace EmojiBot.Api.Models.Facebook
{
	public class Entry
	{
		public string id { get; set; }

		public string time { get; set; }

		public IEnumerable<RecievedMessageModel> messaging { get; set; }

		public Entry()
		{
			messaging = new List<RecievedMessageModel>();
		}
	}
}