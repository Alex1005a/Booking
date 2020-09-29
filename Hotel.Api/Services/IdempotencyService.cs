using Microsoft.Extensions.Caching.Distributed;
using System;

namespace HotelSevice.Api.Services
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly IDistributedCache _cache;
        public IdempotencyService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public string GetKey(string key)
        {
            string cacheValue = _cache.GetString(key);
            return cacheValue;
        }

        public void SetKey(string key, string result)
        {
            _cache.SetString(key, result, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
        }
    }
}
