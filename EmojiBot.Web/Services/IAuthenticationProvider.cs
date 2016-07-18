using EmojiBot.Core.Domain;

namespace EmojiBot.Web.Infrastructure
{
	/// <summary>
	/// Helper methods for signing users in and out
	/// </summary>
	public interface IAuthenticationProvider
	{
		/// <summary>
		/// Creates a user using the given username and password
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		User CreateUser(string username, string password);

		/// <summary>
		/// Validates that a user exists matching the given username and password
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		bool ValidateUser(string username, string password);

		/// <summary>
		/// Signs in a user
		/// </summary>
		/// <param name="username"></param>
		/// <param name="isPersistent"></param>
		void SignIn(string username, bool isPersistent = false);

		/// <summary>
		/// Signs out a user
		/// </summary>
		void SignOut();
	}
}