using Microsoft.Extensions.Caching.Distributed;

namespace CacheAsidePatternExampleWithIDistributedCache;

public static class CacheOptions //for brevity without options pattern and without DI
{
    public static DistributedCacheEntryOptions AbsouluteExpirationInFiveSeconds = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
    };
}
