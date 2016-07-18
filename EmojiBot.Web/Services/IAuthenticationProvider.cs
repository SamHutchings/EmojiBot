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
		/// Gets the authenticated user (if there is one)
		/// </summary>
		/// <returns></returns>
		User GetAuthenticatedUser();

		/// <summary>
		/// Validates that a user exists matching the given username and password
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		bool ValidateUser(string username, string password);

		/// <summary>
		/// Attempts to change a users password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="oldPassword"></param>
		/// <param name="newPassword"></param>
		/// <returns></returns>
		bool ChangePassword(User user, string oldPassword, string newPassword);

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