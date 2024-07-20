using Microsoft.Extensions.Caching.Distributed;

namespace CacheAsidePatternExampleWithIDistributedCache;

public static class CacheExtensions
{
    public static (bool wasCached, string item) GetOrCreate(this IDistributedCache cache, int id, Func<string> stringFactory)
    {
        bool wasCached = true;

        var response = cache.GetString(id.ToString());

        if (string.IsNullOrWhiteSpace(response))
        {
            wasCached = false;

            response = stringFactory();

            cache.SetString(id.ToString(), response, CacheOptions.AbsoluteExpirationInFiveSeconds);
        }

        return (wasCached, response);
    }
}
