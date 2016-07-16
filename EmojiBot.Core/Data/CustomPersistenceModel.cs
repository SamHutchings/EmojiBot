using FluentNHibernate.Automapping;
using System.Reflection;

namespace EmojiBot.Core.Data
{
	public class CustomPersistenceModel : AutoPersistenceModel
	{
		public CustomPersistenceModel() : base(new EmojiBotMappingConfiguration())
		{
			AddEntityAssembly(Assembly.Load("EmojiBot.Core"))
				.Conventions.AddAssembly(Assembly.Load("EmojiBot.Core"));
		}
	}
}
