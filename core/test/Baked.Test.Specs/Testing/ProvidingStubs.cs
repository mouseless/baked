using Baked.Playground.Orm;
using Newtonsoft.Json.Linq;

namespace Baked.Test.Testing;

public class ProvidingStubs : TestSpec
{
    [Test]
    public void Give_me_an_integer_returns_42_because_it_is_the_answer()
    {
        GiveMe.AnInteger().ShouldBe(42);
    }

    [Test]
    public void Give_me_an_email()
    {
        GiveMe.AnEmail().ShouldBe("info@test.com");
    }

    [Test]
    public void String_to_guid_conversion_is_provided_for_convenience()
    {
        var actual = "00000000-0000-0000-0000-000000000000".ToGuid();

        actual.ShouldBe(Guid.Empty);
    }

    [Test]
    public void When_time_is_given_in_FakeTimeProvider_TimeProvider_returns_the_given_time()
    {
        var time = GiveMe.ADateTime(2023, 12, 29, 18, 12, 10);
        MockMe.TheTime(now: time);
        GiveMe.AnEntity(setNowForDateTime: true);
        var entities = GiveMe.The<Entities>();

        var actual = entities.By(dateTime: time);

        actual.Count.ShouldBeGreaterThan(0);
    }

    [Test]
    public void FakeTimeProvider_allows_simulating_of_time_passage()
    {
        var timeProvider = GiveMe.The<TimeProvider>();
        var before = timeProvider.GetNow();

        MockMe.TheTime(passSomeTime: true);

        before.ShouldBeLessThan(timeProvider.GetNow());
    }

    [Test]
    public void Give_me_a_url_and_url_should_be_string()
    {
        var uri = GiveMe.AUrl("https://test.com");

        uri.ShouldBe("https://test.com/");
    }

    [Test]
    public void To_json_string()
    {
        var obj = new { test = "value" };

        var json = obj.ToJsonString();

        json.ShouldBe("""{"test":"value"}""");
    }

    [Test]
    public void To_json_object()
    {
        var obj = new { test = "value" };

        var jobj = obj.ToJsonObject();

        jobj.ShouldBeOfType<JObject>();
    }

    [Test]
    public void Should_deeply_be()
    {
        var obj = new { test = new { nested = "value" } };

        obj.ShouldDeeplyBe(new { test = new { nested = "value" } });
    }

    abstract class Abstract
    {
        public abstract int AbstractProperty { get; }
        public virtual int VirtualProperty { get; }

        public abstract void AbstractMethod();
        public virtual void VirtualMethod() { }
        public abstract void OneParameterMethod(int @int);
    }

    [Test]
    public void Several_reflection_helpers()
    {
        typeof(string).ShouldBe<string>();

        GiveMe.ThePropertyOf<Abstract>(nameof(Abstract.AbstractProperty))?.ShouldBeAbstract();
        GiveMe.ThePropertyOf<Abstract>(nameof(Abstract.VirtualProperty))?.ShouldBeVirtual();

        GiveMe.TheMethodOf<Abstract>(nameof(Abstract.AbstractMethod))?.ShouldBeAbstract();
        GiveMe.TheMethodOf<Abstract>(nameof(Abstract.VirtualMethod))?.ShouldBeVirtual();
        GiveMe.TheMethodOf<Abstract>(nameof(Abstract.OneParameterMethod))?.ShouldHaveOneParameter<int>();
    }
}