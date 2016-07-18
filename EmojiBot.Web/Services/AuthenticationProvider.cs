using EmojiBot.Core.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EmojiBot.Web.Infrastructure
{
	/// <summary>
	/// Helper methods for signing users in and out
	/// </summary>
	public class AuthenticationProvider : IAuthenticationProvider
	{
		private ISession _session;
		private IAuthenticationManager AuthenticationManager
		{
			get { return HttpContext.Current.GetOwinContext().Authentication; }
		}

		public AuthenticationProvider(ISession session)
		{
			_session = session;
		}

		public User CreateUser(string username, string password)
		{
			var user = new User { Email = username, Salt = GetSalt() };

			user.Password = SaltPassword(password, user.Salt);

			return user;
		}

		public User GetAuthenticatedUser()
		{
			if (AuthenticationManager.User != null)
			{
				var user = AuthenticationManager.User.Claims.First().Value;
				return null;
			}

			return null;
		}

		public bool ValidateUser(string username, string password)
		{
			var user = _session.Query<User>().Where(x => x.Email == username).FirstOrDefault();

			if (user == null)
				return false;

			if (SaltPassword(password, user.Salt) == user.Password)
				return true;

			return false;
		}

		public bool ChangePassword(User user, string oldPassword, string newPassword)
		{
			if (SaltPassword(oldPassword, user.Salt) != user.Password)
				return false;

			user.Salt = GetSalt();
			user.Password = SaltPassword(newPassword, user.Salt);

			return true;
		}

		public void SignIn(string username, bool isPersistent = false)
		{
			var claims = new List<Claim>();

			claims.Add(new Claim(ClaimTypes.NameIdentifier, username));

			var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

			AuthenticationManager.SignIn(new AuthenticationProperties()
			{
				AllowRefresh = true,
				IsPersistent = isPersistent,
				ExpiresUtc = DateTime.UtcNow.AddDays(7)
			}, identity);
		}

		public void SignOut()
		{
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
		}

		string SaltPassword(string password, string passwordSalt)
		{
			var passwordBytes = UnicodeEncoding.Unicode.GetBytes(password);
			var saltBytes = UnicodeEncoding.Unicode.GetBytes(password);

			var hashAlgorithm = new HMACSHA256(saltBytes);

			return UnicodeEncoding.Unicode.GetString(hashAlgorithm.ComputeHash(passwordBytes));
		}

		string GetSalt()
		{
			byte[] salt = new byte[32];
			RNGCryptoServiceProvider.Create().GetBytes(salt);

			return UnicodeEncoding.Unicode.GetString(salt);
		}
	}

}