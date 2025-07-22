using Baked.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Baked.Test.Caching;

public class CacheSamples(IMemoryCache _cache, Func<IMemoryCache> _getCache)
{
    [ClientCache("User")]
    public string GetUser() =>
        _getCache().GetOrCreate(nameof(GetUser), _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");

    [ClientCache("Application")]
    public string GetApplication() =>
        _cache.GetOrCreate(nameof(GetApplication), _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");
}