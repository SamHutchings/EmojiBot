using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace EmojiBot.Core.Data.Conventions
{
	public class CustomForeignKeyConvention : ForeignKeyConvention
	{
		protected override string GetKeyName(Member property, Type type)
		{
			return String.Format("{0}_id", type.Name.ToLower());
		}
	}
}
