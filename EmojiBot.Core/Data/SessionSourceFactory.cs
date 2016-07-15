using EmojiBot.Core.Data;
using FluentNHibernate;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace Vibes.Core.Data
{
	public class SessionSourceFactory
	{
		public ISessionSource CreateSessionSource(string connectionString)
		{
			var properties = PostgreSQLConfiguration
				.PostgreSQL82
				.ConnectionString(connectionString)
				.ToProperties();

			return new SessionSource(properties, new CustomPersistenceModel());
		}

		/// <summary>
		/// This method updates the schema against the specified database
		/// </summary>
		public static void DoUpdateSchema()
		{
			// get the session source
			var source = new SessionSourceFactory().CreateSessionSource("Server=localhost;database=emojibot;user id=postgres;password=gr8Vib3s");

			// build the schema
			new SchemaExport(source.Configuration).Execute(true, false, false);
		}
	}
}
