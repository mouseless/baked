using Baked.Authorization;
using Baked.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Baked.Test.Caching;

public class CacheSamples(IMemoryCache _cache, Func<IMemoryCache> _getCache)
{
    string _parameter = default!;

    public CacheSamples With(string parameter)
    {
        _parameter = parameter;

        return this;
    }

    [AllowAnonymous]
    [ClientCache("user")]
    public string GetScoped() =>
        _getCache().GetOrCreate($"{nameof(GetScoped)}[{_parameter}]", _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");

    [AllowAnonymous]
    [ClientCache("application")]
    public string GetApplication() =>
        _cache.GetOrCreate($"{nameof(GetApplication)}[{_parameter}]", _ =>
        {
            return $"{Guid.NewGuid()}";
        }) ?? throw new("cache returns null");
}