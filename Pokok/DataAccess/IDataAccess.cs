using System;
using System.Collections.Generic;

namespace Pokok.DataAccess
{
    public interface IDataAccess : IDisposable
    {
        List<T> LoadData<T>(string sql);
        int SaveData<T>(string sql, T data);
    }
}
