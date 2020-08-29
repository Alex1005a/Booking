using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.Pipelines
{
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IProvideCacheKey, IRequest<TResponse>
    {
        private readonly IDistributedCache _cache;

        public CacheBehavior(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Check in cache if we already have what we're looking for
            
            var cacheKey = request.CacheKey;
            var cacheValue = await _cache.GetStringAsync(cacheKey);
            if (cacheValue != null)
            {               
                return JsonConvert.DeserializeObject<TResponse>(cacheValue);
            }

            // If we don't, execute the rest of the pipeline, and add the result to the cache
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
}
