namespace EmojiBot.Api.Models.Facebook
{
	public class SendMessageModel
	{
		public Recipient recipient { get; set; }

		public Message message { get; set; }
	}
}