using log4net.Repository.Hierarchy;
using SNPIDataManager.Areas.EDCFeed.Helpers;
using SNPIDataManager.Helpers.NopAPIHelper;
using SNPIDataManager.TaskManager;
using SNPIHelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SNPIDataManager
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();

            TaskManager.TaskManager.ProductStockUpateTask();
            TaskManager.TaskManager.FullProductUpdateTask();

            //await RelationsHelper.UpdateProductAttributesScheduled();
        }
    }
}
