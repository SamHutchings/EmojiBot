using EmojiBot.Core.Data;
using EmojiBot.Core.Search;
using FluentNHibernate;
using FluentNHibernate.Cfg.Db;
using log4net;
using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Configuration;

namespace EmojiBot.Web.Infrastructure
{
	public class WebNinjectModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISessionFactory>().ToMethod(c => GetSessionFactory()).InSingletonScope();

			Bind<ILog>().ToMethod(x => LogManager.GetLogger(x.Request.Target.Member.DeclaringType));

			Bind<IAuthenticationProvider>().To<AuthenticationProvider>();

			Bind<IEmojiSearchService>().To<EmojiSearchService>();

			Bind<ISession>().ToMethod(c => c.Kernel.Get<ISessionFactory>().OpenSession())
				.InRequestScope();
		}

		public static ISessionFactory GetSessionFactory()
		{
			var properties = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConfigurationManager.ConnectionStrings["default"].ConnectionString).ToProperties();

			return new SessionSource(properties, new CustomPersistenceModel()).SessionFactory;
		}
	}
}