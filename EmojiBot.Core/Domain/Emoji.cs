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

		public virtual Category Category { get; set; }
		
		/// <summary>
		/// The actual characters for the emoji
		/// </summary>
		public virtual string Characters { get; set; }

		/// <summary>
		/// The name of the emoji, e.g. crying laughing emoji
		/// </summary>
		public virtual string Name { get; set; }

		public virtual string Description { get; set; }

		/// <summary>
		/// The variations for the emoji 
		/// </summary>
		public virtual string Variations { get; set; }

		/// <summary>
		/// Search keywords for the emoji
		/// </summary>
		public virtual string Keywords { get; set; }

		public Emoji()
		{
			Created = DateTime.Now;
		}
	}
}
