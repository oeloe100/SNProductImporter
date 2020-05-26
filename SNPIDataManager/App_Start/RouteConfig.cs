using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SNPIDataManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Submit",
               url: "submit",
               defaults: new { controller = "Authorization", action = "Submit" },
               namespaces: new string[] { "SNPIDataManager.Areas.NopAPIAuthorizer.Controllers" }
            );

            routes.MapRoute(
               name: "GetAccessToken",
               url: "token",
               defaults: new { controller = "Authorization", action = "GetAccessToken" },
               namespaces: new string[] { "SNPIDataManager.Areas.NopAPIAuthorizer.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"SNPIDataManager.Controllers"}
            );
        }
    }
}
