namespace EmojiBot.Api.Models.Facebook.Outbound
{
	public class SendMessageModel
	{
		public User recipient { get; set; }

		public Message message { get; set; }
	}
}