using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomInterface.IService
{
    public interface ICacheService
    {
        Task<T> GetDataAsync<T>(string key);
        Task<bool> SetDataAsync<T>(string key, T data, DateTime expireTime);
        Task<object> RemoveDataAsync(string key);
    }
}
