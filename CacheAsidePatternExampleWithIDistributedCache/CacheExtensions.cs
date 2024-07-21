using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheAsidePatternExampleWithIDistributedCache;

public static class CacheExtensions
{
    public static (string response, bool wasCached, double cachedSinceInSeconds) GetOrCreateWithCacheInfo(
        this IDistributedCache cache, int id, Func<string> stringFactory)
    {
        var cachedJson = cache.GetString(id.ToString());

        if (!string.IsNullOrWhiteSpace(cachedJson))
        {
            var cachedResponseWithTimestamp = JsonSerializer.Deserialize<ResponseWithTimestamp>(cachedJson);
            
            var cachedSinceInSeconds = DateTimeOffset.UtcNow
                .Subtract(cachedResponseWithTimestamp!.CachedAt)
                .TotalSeconds;

            return (cachedResponseWithTimestamp!.Response, true, cachedSinceInSeconds);
        }

        var response = stringFactory();

        var responseWithTimestamp = new ResponseWithTimestamp(response, DateTimeOffset.UtcNow);
        var json = JsonSerializer.Serialize(responseWithTimestamp);

        cache.SetString(id.ToString(), json, CacheOptions.AbsoluteExpirationInFiveSeconds);

        return (response, false, 0);
    }
}
