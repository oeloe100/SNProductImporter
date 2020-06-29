using SNPIDataManager.Areas.EDCFeed.Models.MappingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNPIDataManager.Areas.EDCFeed.Controllers.API
{
    public class SaveMappingsController : ApiController
    {
        public static bool SaveMappings(List<MappingModel> mappingsList) 
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                //Log error > ex.StackTrace
                return false;
            }
        }
    }
}
