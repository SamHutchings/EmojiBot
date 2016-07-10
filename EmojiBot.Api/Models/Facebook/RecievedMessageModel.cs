namespace EmojiBot.Api.Models.Facebook
{
	public class RecievedMessageModel
	{
		public User sender { get; set; }
	
		public User recipient { get; set; }

		public InboundMessage message { get; set; }
	}
}