namespace EmojiBot.Core.Domain
{
	using System;

	public class Category
	{
		public virtual int Id { get; set; }

		public virtual DateTime Created { get; set; }

		public virtual int SortOrder { get; set; }

		public virtual string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
