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
        private static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        /// <summary>
        /// Delete provided data from database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static List<T> LoadData<T>(string sql, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(connectionString)))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        /// <summary>
        /// Save provided data to database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="data"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static int SaveData<T>(string sql, T data, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(connectionString)))
            {
                return cnn.Execute(sql, data);
            }
        }

        //*** Delete Methods has no BusinessLogic involved ***\\

        /// <summary>
        /// Delete Single Mapping
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static int DeleteMapping(int Id, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(connectionString)))
            {
                return cnn.Execute(@"DELETE FROM dbo.EDCMappings WHERE id = @Id", new { id = Id });
            }
        }

        /// <summary>
        /// Delete All/Multiple Mappings
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
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
