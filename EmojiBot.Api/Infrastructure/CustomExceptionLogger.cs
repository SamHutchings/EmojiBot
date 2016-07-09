using log4net;
using System;
using System.Linq;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace EmojiBot.Api.Infrastructure
{
	public class CustomExceptionLogger : ExceptionLogger
	{
		static readonly ILog __log = LogManager.GetLogger(typeof(CustomExceptionLogger));

		public override void Log(ExceptionLoggerContext context)
		{
			var sb = new StringBuilder();

			sb.AppendLine();
			sb.AppendLine();

			if (context.RequestContext != null)
			{
				var request = context.RequestContext;

				sb.AppendFormat("Url: {0}", context.Request.RequestUri);

				if (request.Principal != null && request.Principal.Identity != null && !String.IsNullOrWhiteSpace(request.Principal.Identity.Name))
				{
					sb.AppendLine();
					sb.AppendFormat("User: {0}", request.Principal.Identity.Name);
				}

				sb.AppendLine();
				sb.AppendFormat("Route: {0}", String.Join(", ", request.RouteData.Values.Select(x => String.Format("{0}:{1}", x.Key, x.Value))));
			}
			
			sb.AppendLine();
			sb.AppendLine();

			__log.Error(sb.ToString(), context.Exception);

			base.Log(context);
		}
	}
}