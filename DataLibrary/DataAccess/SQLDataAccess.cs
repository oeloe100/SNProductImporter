using Dapper;
using System.Data;
using System.Data.SqlClient;

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
    }
}
