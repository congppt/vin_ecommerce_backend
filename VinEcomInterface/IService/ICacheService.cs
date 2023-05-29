using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinEcomInterface.IService
{
    public interface ICacheService
    {
        Task<T> GetData<T>(string key);
        Task<bool> SetData<T>(string key, T data, DateTime expireTime);
        Task<object> RemoveData(string key);
    }
}
