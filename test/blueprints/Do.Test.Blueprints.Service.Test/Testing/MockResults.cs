namespace Do.Test.Testing;

public class MockResults : TestServiceSpec
{
    [Test]
    public void Mock_object_returns_the_next_value_after_each_call()
    {
        var mock = new Mock<IInterface>();
        var setup = () => mock.Setup(c => c.TestObject(It.IsAny<object>()));
        setup().Returns(["first", "second"]);

        var firstResponse = mock.Object.TestObject(this);
        var secondResponse = mock.Object.TestObject(this);

        firstResponse.ShouldBe("first");
        secondResponse.ShouldBe("second");
    }

    [Test]
    public async Task Mock_object_returns_the_next_value_after_each_async_call()
    {
        var mock = new Mock<IInterface>();
        var setup = () => mock.Setup(c => c.TestAsyncObject(It.IsAny<object>()));
        setup().ReturnsAsync(["first", "second"]);

        var firstResponse = await mock.Object.TestAsyncObject(this);
        var secondResponse = await mock.Object.TestAsyncObject(this);

        firstResponse.ShouldBe("first");
        secondResponse.ShouldBe("second");
    }
}
