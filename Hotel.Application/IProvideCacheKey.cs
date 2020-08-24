using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application
{
    public interface IProvideCacheKey
    {
        string CacheKey { get; }
    }
}
