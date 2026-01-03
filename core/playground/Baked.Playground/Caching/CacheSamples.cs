using Baked.Authorization;
using Baked.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Baked.Playground.Caching;

[AllowAnonymous]
public class CacheSamples(IMemoryCache _cache, Func<IMemoryCache> _getCache)
{
    CacheKey _parameter = default!;

    public CacheSamples With(CacheKey parameter)
    {
        _parameter = parameter;

        return this;
    }

    [ClientCache("user")]
    public string GetScoped() =>
        _getCache().GetOrCreate($"{nameof(GetScoped)}[{_parameter}]", _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");

    [ClientCache("application")]
    public string GetApplication() =>
        _cache.GetOrCreate($"{nameof(GetApplication)}[{_parameter}]", _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");
}