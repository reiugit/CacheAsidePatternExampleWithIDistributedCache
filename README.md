# Cache-Aside Pattern example using IDistributedCache.

Topics:
* Adding DistributedMemoryCache to DI Container
* Injecting IDistributedCache into endpoint
* Usage of 'IDistributedCache.GetString(key)'
* Usage of 'IDistributedCache.SetString(key, string, CacheOptions)'
* Expiration/Eviction

This can be used to cache serialized objects in the form of json strings.
