using Do.Caching;
using Do.Caching.ScopedMemory;

namespace Do;

public static class ScopedMemoryCachingExtensions
{
    public static ScopedMemoryCachingFeature ScopedMemory(this CachingConfigurator _) => new();
}
