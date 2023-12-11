using Do.Caching;
using Do.Caching.ScopedMemory;

namespace Do;

public static class ScopedMemoryExtensions
{
    public static ScopedMemoryFeature ScopedMemory(this CachingConfigurator _) => new();
}
