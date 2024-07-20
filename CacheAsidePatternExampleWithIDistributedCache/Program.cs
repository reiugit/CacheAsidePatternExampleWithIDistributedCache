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

    if (string.IsNullOrWhiteSpace(response))
    {
        isCached = false;
        
        response = $"Response for Id {id}";

        cache.SetString(id.ToString(), response, CacheOptions.AbsoluteExpirationInFiveSeconds);
    }

    return new { id, response, isCached };
});

app.Run();

