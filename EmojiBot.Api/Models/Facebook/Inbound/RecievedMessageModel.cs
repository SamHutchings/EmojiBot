namespace EmojiBot.Api.Models.Facebook.Inbound
{
	public class RecievedMessageModel
	{
		public User sender { get; set; }
	
		public User recipient { get; set; }

		public Message message { get; set; }
	}
}