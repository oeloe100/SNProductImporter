using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using SNPIDataManager.Areas.EDCFeed.Models.MappingModels;
using SNPIDataManager.Helpers;
using SNPIDataLibrary.BusinessLogic;
using System.Threading.Tasks;

namespace SNPIDataManager.Areas.EDCFeed.Helpers
{
    public class SaveMappings
    {
        private MappingProcessor mappingProcessor = new MappingProcessor();
        private UserInformation userInfo = new UserInformation();
        private List<MappingModel> MappingList;

        SaveMappings(List<MappingModel> mappingList) 
        {
            MappingList = mappingList;
        }

        private bool IsSaved() 
        {
            try
            {
                mappingProcessor.InsterCreatedMapping(null, userInfo.UserId());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}