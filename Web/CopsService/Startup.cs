﻿using Hangfire;
using Owin;

namespace PublicService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureHangfire(app);
        }
    }
}