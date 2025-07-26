using Baked.Caching;
using Baked.Caching.InMemory;
using Baked.Runtime;
using Microsoft.Extensions.Caching.Memory;

namespace Baked;

public static class InMemoryCachingExtensions
{
    public static InMemoryCachingFeature InMemory(this CachingConfigurator _,
        Action<MemoryCacheOptions>? options = default,
        Setting<TimeSpan>? clientExpiration = default
    ) => new(options ?? (_ => { }), clientExpiration ?? TimeSpan.FromHours(1));
}