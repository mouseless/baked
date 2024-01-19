namespace Do.Test.Testing;

public class MockResults : TestServiceSpec
{
    [Test]
    public void Mock_object_returns_the_next_value_after_each_call()
    {
        var client = MockMe.AMockedObject(["first", "second"]);

        var firstResponse = client.DoSomething();
        var secondResponse = client.DoSomething();

        firstResponse.ShouldBe("first");
        secondResponse.ShouldBe("second");
    }

    [Test]
    public async Task Mock_object_returns_the_next_value_after_each_async_call()
    {
        var client = MockMe.AMockedObject(["first", "second"]);

        var firstResponse = await client.DoSomethingTask();
        var secondResponse = await client.DoSomethingTask();

        firstResponse.ShouldBe("first");
        secondResponse.ShouldBe("second");
    }
}
