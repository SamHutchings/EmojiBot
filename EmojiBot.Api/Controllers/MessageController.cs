using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EmojiBot.Api.Controllers
{
	[AllowAnonymous]
	public class MessageController : ApiController
	{
		// POST api/values
		public IHttpActionResult Post([FromBody]string value)
		{
			return Ok("it's ok");
		}

	}
}