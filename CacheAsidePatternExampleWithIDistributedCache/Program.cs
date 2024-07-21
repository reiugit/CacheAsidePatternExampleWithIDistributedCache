using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using CacheAsidePatternExampleWithIDistributedCache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.MapGet("/test-cacheaside/{id:int}", (int id, [FromServices] IDistributedCache cache) =>
{
    return cache.GetOrCreateWithCacheInfo(id, factory: () => $"Response for Id {id} ...");
});

app.Run();
