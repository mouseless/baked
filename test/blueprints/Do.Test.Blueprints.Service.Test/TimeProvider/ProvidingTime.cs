namespace Do.Test.ExceptionHandling;

public class ProvidingTime : TestServiceSpec
{
    [Test]
    public void When_time_is_given_in_FakeTimeProvider_TimeProvider_returns_the_given_time()
    {
        var time = GiveMe.ADateTime(2023, 12, 10, 10, 10, 10);
        MockMe.TheTime(now: time);
        GiveMe.AnEntity(setNowForDateTime: true);
        var entities = GiveMe.The<Entities>();

        var actual = entities.By(dateTime: time);

        actual.Count.ShouldBeGreaterThan(0);
    }
}
