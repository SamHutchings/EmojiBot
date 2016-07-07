using log4net;
using System.Web.Http;

namespace EmojiBot.Api.Controllers
{
	public class BaseApiController : ApiController
	{
		static readonly ILog __log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	}
}