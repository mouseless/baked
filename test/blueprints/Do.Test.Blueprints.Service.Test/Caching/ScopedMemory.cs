using Microsoft.Extensions.Caching.Memory;

namespace Do.Test.Caching;

public class ScopedMemory : TestServiceSpec
{
    IMemoryCache _cache = default!;

    public override void SetUp()
    {
        base.SetUp();

        _cache = GiveMe.AMemoryCache();
    }

    public override void TearDown()
    {
        base.TearDown();

        _cache.VerifyCount(c => c is 0);
    }

    [Test]
    public void Objects_can_be_cached_in_memory()
    {
        var expected = _cache.GetOrCreate<object>("key", _ => new());

        var actual = _cache.GetOrCreate<object>("key", _ => new());

        actual.ShouldBeSameAs(expected);
        _cache.VerifyCount(c => c is 1);
    }
}
