namespace EmojiBot.Api.Services
{
	using log4net;
	using Models.Facebook.Inbound;
	using Models.Facebook.Outbound;

	/// <summary>
	/// Facebook service that doesn't talk to facebook, for local testing
	/// </summary>
	public class DisconnectedFacebookGraphService : IFacebookGraphService
	{
		static readonly ILog __log = LogManager.GetLogger(typeof(FacebookGraphService));

		public DisconnectedFacebookGraphService()
		{
		}

		public bool SendMessage(string recipientId, string text)
		{
			return true;
		}

		public UserDetails GetUserDetails(string id)
		{
			return new UserDetails { first_name = "No", last_name = "User" };
		}
	}
}
