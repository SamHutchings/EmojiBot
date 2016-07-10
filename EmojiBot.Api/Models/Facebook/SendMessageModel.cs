namespace EmojiBot.Api.Models.Facebook
{
	public class SendMessageModel
	{
		public User recipient { get; set; }

		public OutboundMessage message { get; set; }
	}
}