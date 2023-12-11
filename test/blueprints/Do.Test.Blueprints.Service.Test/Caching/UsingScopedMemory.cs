using Microsoft.Extensions.Caching.Memory;

namespace Do.Test.Caching;

public class UsingScopedMemory : TestServiceSpec
{
    public override void TearDown()
    {
        base.TearDown();

        GiveMe.AMemoryCache().ShouldHaveCount(0);
    }

    [Test]
    public void Objects_can_be_cached_in_memory()
    {
        var cache = GiveMe.AMemoryCache();
        var expected = cache.GetOrCreate<object>("key", _ => new());

        var actual = cache.GetOrCreate<object>("key", _ => new());

        actual.ShouldBeSameAs(expected);
        cache.ShouldHaveCount(1);
    }
}
