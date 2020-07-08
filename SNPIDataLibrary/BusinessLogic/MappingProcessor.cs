using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNPIDataLibrary.DataAccess;
using SNPIDataLibrary.Models;

namespace SNPIDataLibrary.BusinessLogic
{
    public static class MappingProcessor
    {
        public static int InsterCreatedMapping(MappingModel model)
        {
            string sql;

            sql = @"INSERT INTO dbo.EDCMappings (userId, shopCategory, shopCategoryId, supplierCategory, supplierCategoryId) VALUES (@id, @shopCategory, @shopCategoryId, @supplierCategory, @supplierCategoryId);";

            return SQLDataAccess.SaveData<MappingModel>(sql, model, "SNPI_Mappings_db");
        }

        public static List<MappingModel> RetrieveMapping <MappingModel>() 
        {
            string sql = @"SELECT id, shopCategory, shopCategoryId, supplierCategory, supplierCategoryId FROM dbo.EDCMappings";
            return SQLDataAccess.LoadData<MappingModel>(sql, "SNPI_Mappings_db");
        }
    }
}
