using Microsoft.Extensions.Caching.Memory;

namespace Baked.Test.Caching;

public class UsingScopedMemory : TestServiceSpec
{
    [Test]
    public void Objects_can_be_cached_in__scoped_memory()
    {
        var cache = GiveMe.AMemoryCache();
        var expected = cache.GetOrCreate<object>("key", _ => new());

        var actual = cache.GetOrCreate<object>("key", _ => new());

        actual.ShouldBeSameAs(expected);
        cache.ShouldHaveCount(1);
    }
}