namespace Do.Test.Testing;

public class MockResults : TestServiceSpec
{
    public interface IMockedInterface
    {
        object TestObject(object request);
        Task<object> TestAsyncObject(object request);
    }

    [Test]
    public void Mock_object_returns_the_next_value_after_each_call()
    {
        var mock = new Mock<IMockedInterface>();
        var setup = () => mock.Setup(c => c.TestObject(It.IsAny<object>()));
        setup().Returns(["first", "second"]);

        mock.Object.TestObject(this).ShouldBe("first");
        mock.Object.TestObject(this).ShouldBe("second");
        mock.Object.TestObject(this).ShouldBe("first");
    }

    [Test]
    public async Task Mock_object_returns_the_next_value_after_each_async_call()
    {
        var mock = new Mock<IMockedInterface>();
        var setup = () => mock.Setup(c => c.TestAsyncObject(It.IsAny<object>()));
        setup().ReturnsAsync(["first", "second"]);

        (await mock.Object.TestAsyncObject(this)).ShouldBe("first");
        (await mock.Object.TestAsyncObject(this)).ShouldBe("second");
        (await mock.Object.TestAsyncObject(this)).ShouldBe("first");
    }
}
