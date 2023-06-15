using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface.IService;

namespace VinEcomService.Service
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly ITimeService _timeService;
        public RedisCacheService(IConfiguration config, ITimeService timeService)
        {
            var redisConfig = config.GetSection("Redis");
            var redisOptions = new ConfigurationOptions
            {
                EndPoints = { { redisConfig["Host"], int.Parse(redisConfig["Port"]) } },
                Ssl = true,
                SslProtocols = SslProtocols.Tls12,
                AbortOnConnectFail = false,
                User = redisConfig["Username"],
                Password = redisConfig["Password"],
            };
            _db = ConnectionMultiplexer.Connect(redisOptions).GetDatabase();
            _timeService = timeService;
        }
        public async Task<T> GetDataAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (string.IsNullOrEmpty(value)) return default;
            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<object> RemoveDataAsync(string key)
        {
            var isExist = await _db.KeyExistsAsync(key);
            if (isExist) return await _db.KeyDeleteAsync(key);
            return false;
        }

        public async Task<bool> SetDataAsync<T>(string key, T data, DateTime expireTime)
        {
            var timeSpan = expireTime.Subtract(_timeService.GetCurrentTime());
            var isSet = await _db.StringSetAsync(key, JsonConvert.SerializeObject(data), timeSpan);
            return isSet;
        }
    }
}
