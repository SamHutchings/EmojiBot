using EmojiBot.Api.Services;
using EmojiBot.Core.Data;
using EmojiBot.Core.Extensions;
using FluentNHibernate;
using FluentNHibernate.Cfg.Db;
using log4net;
using NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Configuration;
using EmojiBot.Core.Search;

namespace EmojiBot.Api.Infrastructure
{
	public class ApiNinjectModule : NinjectModule
	{
		public override void Load()
		{
			if (ConfigurationManager.AppSettings["disable-facebook"].ToBoolean(false))
			{
				Bind<IFacebookGraphService>().To<DisconnectedFacebookGraphService>();
			}
			else
			{
				Bind<IFacebookGraphService>().To<FacebookGraphService>();
			}

			Bind<IEmojiSearchService>().To<EmojiSearchService>();

			Bind<ISessionFactory>().ToMethod(c => GetSessionFactory()).InSingletonScope();

			Bind<ILog>().ToMethod(x => LogManager.GetLogger(x.Request.Target.Member.DeclaringType));

			Bind<ISession>().ToMethod(c => c.Kernel.Get<ISessionFactory>().OpenSession())
				.InRequestScope()
				.OnActivation(s => s.BeginTransaction())
				.OnDeactivation((s) =>
				{
					try
					{
						s.Transaction.Commit();
					}
					catch (Exception e)
					{
						Kernel.Get<ILog>().Error(e);

						s.Transaction.Rollback();
					}

					s.Close();
					s.Dispose();
				});
		}

		public static ISessionFactory GetSessionFactory()
		{
			var properties = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConfigurationManager.ConnectionStrings["default"].ConnectionString).ToProperties();

			return new SessionSource(properties, new CustomPersistenceModel()).SessionFactory;
		}
	}
}