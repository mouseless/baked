using Baked.Authorization;
using Baked.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Baked.Test.Caching;

public class CacheSamples(IMemoryCache _cache, Func<IMemoryCache> _getCache)
{
    [AllowAnonymous]
    [ClientCache("user")]
    public string GetScoped() =>
        _getCache().GetOrCreate(nameof(GetScoped), _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");

    [AllowAnonymous]
    [ClientCache("application")]
    public string GetApplication() =>
        _cache.GetOrCreate(nameof(GetApplication), _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");
}