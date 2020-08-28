using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Application.Pipelines
{
    public class RemoveCacheBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    where TRequest : ICacheRemove, IRequest<TResponse>
    {
        private readonly IDistributedCache _cache;

        public RemoveCacheBehavior(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            string cacheKey = request.CacheKey;
            await _cache.RemoveAsync(cacheKey);
        }
    }
}
