using Microsoft.Extensions.Caching.Memory;

namespace Do.Test.Caching;

public class ScopedMemory : TestServiceSpec
{
    public override void TearDown()
    {
        base.TearDown();

        var cache = GiveMe.AMemoryCache();
        cache.VerifyCount(c => c is 0);
    }

    [Test]
    public void Objects_can_be_cached_in_memory()
    {
        var cache = GiveMe.AMemoryCache();
        var expected = GiveMe.AMemoryCache().GetOrCreate<object>("key", _ => new());

        var actual = cache.GetOrCreate<object>("key", _ => new());

        actual.ShouldBeSameAs(expected);
        cache.VerifyCount(c => c is 1);
    }
}
