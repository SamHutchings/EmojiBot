using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EmojiBot.Startup))]

namespace EmojiBot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
