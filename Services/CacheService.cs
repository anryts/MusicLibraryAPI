using Microsoft.Extensions.Caching.Memory;

namespace MusicLibraryAPI.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set(string key, object value, int minutes)
    {
        _cache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
        });
    }

    public T Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}