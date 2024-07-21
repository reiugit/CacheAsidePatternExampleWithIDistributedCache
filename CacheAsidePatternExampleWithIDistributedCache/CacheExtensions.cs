using CacheAsidePatternExampleWithIMemoryCache;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheAsidePatternExampleWithIDistributedCache;

public static class CacheExtensions
{
    public static ResponseWithCacheInfo GetOrCreateWithCacheInfo(
        this IDistributedCache cache, int id, Func<string> factory)
    {
        var cachedJson = cache.GetString(id.ToString());

        if (!string.IsNullOrWhiteSpace(cachedJson))
        {
            var cachedResponseWithTimestamp = JsonSerializer.Deserialize<ResponseWithTimestamp>(cachedJson);
            
            var cachedSinceInSeconds = DateTimeOffset.UtcNow
                .Subtract(cachedResponseWithTimestamp!.CachedAt)
                .TotalSeconds;

            return new(id, cachedResponseWithTimestamp!.Response, true, cachedSinceInSeconds);
        }

        var response = factory();

        var responseWithTimestamp = new ResponseWithTimestamp(response, DateTimeOffset.UtcNow);
        var json = JsonSerializer.Serialize(responseWithTimestamp);

        cache.SetString(id.ToString(), json, CacheOptions.AbsoluteExpirationInFiveSeconds);

        return new(id, response, false, 0);
    }
}
