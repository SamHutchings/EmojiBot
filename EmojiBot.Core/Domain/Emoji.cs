namespace EmojiBot.Core.Domain
{
	using System;

	/// <summary>
	/// Represents an emoji
	/// </summary>
	public class Emoji
	{
		public virtual int Id { get; set; }

		public virtual DateTime Created { get; set; }

		public virtual string EmojiCharacters { get; set; }

		public virtual string Description { get; set; }

		public virtual string Keywords { get; set; }

		public Emoji()
		{
			Created = DateTime.Now;
		}
	}
}
