using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNPIDataLibrary.DataAccess;
using SNPIDataLibrary.Models;

namespace SNPIDataLibrary.BusinessLogic
{
    public class MappingProcessor
    {
        public int InsterCreatedMapping(MappingModel model)
        {
            string sql;

            var mappingModel = new MappingModel()
            {
                id = model.id,
                shopCategory = model.shopCategory,
                shopCategoryId = model.supplierCategoryId,
                supplierCategory = model.supplierCategory,
                supplierCategoryId = model.shopCategoryId
            };

            sql = @"INSERT INTO dbo.EDCMappings (userId, shopCategory, shopCategoryId, supplierCategory, supplierCategoryId) VALUES (@id, @shopCategory, @shopCategoryId, @supplierCategory, @supplierCategoryId);";

            return SQLDataAccess.SaveData<MappingModel>(sql, mappingModel, "SNPI_Mappings_db");
        }

        public List<MappingModel> RetrieveMapping <MappingModel>() 
        {
            string sql = @"SELECT id, shopCategory, shopCategoryId, supplierCategory, supplierCategoryId FROM dbo.EDCMappings";
            return SQLDataAccess.LoadData<MappingModel>(sql, "SNPI_Mappings_db");
        }
    }
}
