using System;

namespace EmojiBot.Core.Domain
{
	public class User
	{
		public virtual int Id { get; set; }

		public virtual DateTime Created { get; set; }

		public virtual string Email { get; set; }

		public virtual string Password { get; set; }

		public virtual string Salt { get; set; }

		public User()
		{
			Created = DateTime.Now;
		}
	}
}
