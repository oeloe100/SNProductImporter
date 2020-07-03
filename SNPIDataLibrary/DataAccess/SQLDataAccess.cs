using System;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace SNPIDataLibrary.DataAccess
{
    public static class SQLDataAccess
    {
        public static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(connectionString)))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static int SaveData<T>(string sql, T data, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(connectionString)))
            {
                return cnn.Execute(sql, data);
            }
        }

        public static int DeleteMapping(int Id, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(connectionString)))
            {
                return cnn.Execute(@"DELETE FROM dbo.EDCMappings WHERE id = @Id", new { id = Id });
            }
        }

        public static string DeleteMappings(List<int> idList, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(connectionString)))
            {
                foreach(var Id in idList)
                    cnn.Execute(@"DELETE FROM dbo.EDCMappings WHERE id = @Id", new { id = Id });

                return "Done";
            }
        }
    }
}
