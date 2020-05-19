using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SNPIDataManager.Areas.NopAPIAuthorizer.Managers
{
    public class DbManager
    {
        public bool CheckConnection(string connString)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch
                {
                    //connection.Close(): > no need for close statement in using clause.
                    return false;
                }
            }
        }
    }
}