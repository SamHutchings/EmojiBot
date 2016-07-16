using FluentNHibernate;
using FluentNHibernate.Automapping;
using System;

namespace EmojiBot.Core.Data
{
	public class EmojiBotMappingConfiguration : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap(Type type)
		{
			if (type.Namespace.StartsWith("EmojiBot.Core.Domain"))
				return true;

			return false;
		}

		public override bool ShouldMap(Member member)
		{
			if (!member.IsPublic)
				return false;

			if (!member.CanWrite)
				return false;

			return base.ShouldMap(member);
		}
	}
}
