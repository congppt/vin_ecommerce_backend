using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface.IService;

namespace VinEcomService.Service
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly ITimeService _timeService;
        public RedisCacheService(IConfiguration configuration, ITimeService timeService)
        {
            _db = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")).GetDatabase();
            _timeService = timeService;
        }
        public async Task<T> GetData<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (string.IsNullOrEmpty(value)) return default;
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<object> RemoveData(string key)
        {
            var isExist = await _db.KeyExistsAsync(key);
            if (isExist) return await _db.KeyDeleteAsync(key);
            return false;
        }

        public async Task<bool> SetData<T>(string key, T data, DateTime expireTime)
        {
            var timeSpan = expireTime.Subtract(_timeService.GetCurrentTime());
            var isSet = await _db.StringSetAsync(key, JsonConvert.SerializeObject(data), timeSpan);
            return isSet;
        }
    }
}
