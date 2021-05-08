using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace Pokok.DataAccess
{
    public class SqlDataAccess
    {
        private string ConnectionString { get; set; }

        public SqlDataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<T> LoadData<T>(string sql)
        {
            using IDbConnection cnn = new SqlConnection(ConnectionString);
            return cnn.Query<T>(sql).ToList();
        }

        public int SaveData<T>(string sql, T data)
        {
            using IDbConnection cnn = new SqlConnection(ConnectionString);
            return cnn.Execute(sql, data);
        }
    }
}
