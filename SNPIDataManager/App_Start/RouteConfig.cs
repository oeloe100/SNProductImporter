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
                name: "InsertMapping",
                url: "MappingMiddelware/InsertMapping",
                defaults: new { controller = "InsertMapping", action = "InsertMappingModel" },
                namespaces: new[] { "SNPIDataManager.Areas.EDCFeed.Controllers" }
            );

            routes.MapRoute(
                name: "Submit",
                url: "Submit",
                defaults: new { controller = "NopAuthorization", action = "Authorize" },
                namespaces: new[] { "SNPIDataManager.Controllers" }
            );

            routes.MapRoute(
                name: "TokenEP",
                url: "NopAuthorize",
                defaults: new { controller = "NopAuthorization", action = "ReceiveAccessToken"},
                namespaces: new[] { "SNPIDataManager.Controllers" }
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
