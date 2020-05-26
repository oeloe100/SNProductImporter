using System.Web.Mvc;

namespace SNPIDataManager.Areas.NopAPIAuthorizer
{
    public class NopAPIAuthorizerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NopAPIAuthorizer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

        }
    }
}