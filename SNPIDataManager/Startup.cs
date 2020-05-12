using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(SNPIDataManager.Startup))]

namespace SNPIDataManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
