using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Pokok.DataAccess
{
    public class SqlDataAccess : IDataAccess
    {
        private string ConnectionString { get; set; }

        public SqlDataAccess(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("PokokDB");
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

        public void Dispose()
        {
        }
    }
}
