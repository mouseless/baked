using Baked.Caching;
using Baked.Caching.InMemory;
using Microsoft.Extensions.Caching.Memory;

namespace Baked;

public static class InMemoryCachingExtensions
{
    public static InMemoryCachingFeature InMemory(this CachingConfigurator _,
        Action<MemoryCacheOptions>? options = default
    ) => new(options ?? (_ => { }));
}