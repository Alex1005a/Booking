using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HotelSevice.Application.Pipelines
{
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IDistributedCache _cache;

        public CacheBehavior(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if(!(request is IProvideCacheKey))
            {
                return await next();
            }

            var requestProvideKey = request as IProvideCacheKey;

            var cacheKey = requestProvideKey.CacheKey;
            var cacheValue = await _cache.GetStringAsync(cacheKey);
            if (cacheValue != null)
            {               
                return JsonConvert.DeserializeObject<TResponse>(cacheValue);
            }

            var response = await next();

            if(response != null)
            {
                await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(response), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });
            }           

            return response;
        }
    }

    public interface IProvideCacheKey
    {
        string CacheKey { get; }
    }
}
