using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using CacheAsidePatternExampleWithIDistributedCache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.MapGet("/test-cacheaside/{id:int}", (int id, [FromServices] IDistributedCache cache) =>
{
    bool isCached = true;

    var response = cache.GetString(id.ToString());

    if (response == null)
    {
        isCached = false;
        cache.SetString(id.ToString(), $"Response for Id {id}", CacheOptions.AbsouluteExpirationInFiveSeconds);
    }

    return new { id, response, isCached };
});

app.Run();

