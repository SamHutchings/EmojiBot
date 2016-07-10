using EmojiBot.Api.Models.Facebook;

namespace EmojiBot.Api.Services
{
	/// <summary>
	/// Service to interact with the facebook graph API
	/// </summary>
	public interface IFacebookGraphService
	{
		/// <summary>
		/// Sends a plain text message to the given recipient
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		bool SendMessage(SendMessageModel model);
	}
}
