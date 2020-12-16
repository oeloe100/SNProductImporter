using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLibrary.DataAccess
{
    class SQLDataAccess
    {
        public static int SaveData<T>(string sql, T data, string connectionString)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                return cnn.Execute(sql, data);
            };
        }

        public static List<T> LoadData<T>(string sql, string connectionString)
        {
            using(IDbConnection cnn = new SqlConnection(connectionString))
            {
                return cnn.Query<T>(sql).ToList();
            };
        }
    }
}
