using Microsoft.Extensions.Caching.Memory;

namespace Baked.Test.Caching;

public class UsingInMemory : TestSpec
{
    [Test]
    public void Objects_can_be_cached_in_memory()
    {
        var cache = GiveMe.TheMemoryCache();
        var expected = cache.GetOrCreate<object>("key", _ => new());

        var actual = cache.GetOrCreate<object>("key", _ => new());

        actual.ShouldBeSameAs(expected);
        cache.ShouldHaveCount(1);
    }
}