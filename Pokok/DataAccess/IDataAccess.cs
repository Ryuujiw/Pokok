using Dapper;
using System;
using System.Collections.Generic;

namespace Pokok.DataAccess
{
    public interface IDataAccess : IDisposable
    {
        List<T> LoadData<T>(string sql);
        List<T> LoadData<T>(string sql, DynamicParameters dbArgs);
        int SaveData<T>(string sql, T data);
    }
}
