using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using CacheAsidePatternExampleWithIDistributedCache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.MapGet("/test-cacheaside/{id:int}", (int id, [FromServices] IDistributedCache cache) =>
{
    var (wasCached, response) = cache.GetOrCreate(id, () => $"Response for Id {id}");

    return new { id, response, wasCached };
});

app.Run();
