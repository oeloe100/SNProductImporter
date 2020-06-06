using System.Web.Mvc;

namespace SNPIDataManager.Areas.EDCFeed
{
    public class EDCFeedAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EDCFeed";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EDCFeed_default",
                "EDCFeed/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}