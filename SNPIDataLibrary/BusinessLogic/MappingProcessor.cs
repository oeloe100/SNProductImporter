using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNPIDataLibrary.DataAccess;
using SNPIDataManager.Areas.EDCFeed.Models.MappingModels;

namespace SNPIDataLibrary.BusinessLogic
{
    public class MappingProcessor
    {
        public int InsterCreatedMapping(MappingModel model, string id)
        {
            string sql;

            var mappingModel = new MappingModel()
            {
                id = id,
                shopCategory = model.shopCategory,
                shopId = model.shopId,
                supplierCategory = model.supplierCategory,
                supplierId = model.supplierId
            };

            sql = @"INSERT INTO dbo.EDCMappings (id, shopCategory, shopId, supplierCategory, supplierId) VALUES (@id, @shopCategory, @shopId, @supplierCategory, @supplierId);";

            return SQLDataAccess.SaveData<MappingModel>(sql, mappingModel);
        }
    }
}
