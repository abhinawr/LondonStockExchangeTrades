using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace LondonStockExchangeTrades.Infrastructure.Caching;

public class InMemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public InMemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<T> GetAsync<T>(string key) => Task.FromResult(_cache.Get<T>(key))!;
    public Task SetAsync<T>(string key, T value, TimeSpan expiry)
    {
        _cache.Set(key, value, expiry);
        return Task.CompletedTask;
    }
}
