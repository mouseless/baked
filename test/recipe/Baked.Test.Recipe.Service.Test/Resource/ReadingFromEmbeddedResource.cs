namespace Baked.Test.Resource;

class ReadingFromEmbeddedResource : TestServiceSpec
{
    [Test]
    public void Contents_of_an_embedded_resource_can_be_read() => this.ShouldFail();
}
