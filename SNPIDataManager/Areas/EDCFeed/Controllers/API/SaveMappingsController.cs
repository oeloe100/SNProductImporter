using System;
using System.Collections.Generic;
using System.Web.Http;
using SNPIDataLibrary.BusinessLogic;
using SNPIDataManager.Helpers;
using SNPIDataManager.Areas.EDCFeed.Models.MappingModels;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;
using Newtonsoft.Json.Serialization;
using SNPIDataManager.Areas.EDCFeed.Models.CategoryModels;

namespace SNPIDataManager.Areas.EDCFeed.Controllers.API
{
    public class SaveMappingsController : ApiController
    {
        //private MappingProcessor mappingProcessor = new MappingProcessor();
        //private UserInformation userInfo = new UserInformation();
        //private readonly List<MappingModel> MappingList;

        [HttpPost]
        [Route("Mapping/CreateMapping")]
        public IHttpActionResult PostCreatedMapping([FromBody] CategoryModel model)
        {
            Console.WriteLine(model);
            //MappingProcessor mappingProcessor = new MappingProcessor();

            try
            {
                //mappingProcessor.InsterCreatedMapping(null, null)
                if (model == null)
                    return InternalServerError();
                    
                return Ok(200);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
