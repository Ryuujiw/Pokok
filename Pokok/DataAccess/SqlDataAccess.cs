using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Reflection.Metadata;

namespace Pokok.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "PokokDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using IDbConnection cnn = new SqlConnection(GetConnectionString());
            return cnn.Query<T>(sql).ToList();
        }

        public static int SaveData<T>(string sql, T data)
        {
            using IDbConnection cnn = new SqlConnection(GetConnectionString());
            return cnn.Execute(sql, data);
        }
    }
}
