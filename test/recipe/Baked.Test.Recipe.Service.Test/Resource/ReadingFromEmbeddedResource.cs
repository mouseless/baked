using Baked.Resource;

namespace Baked.Test.Resource;

class ReadingFromEmbeddedResource : TestServiceSpec
{
    [Test]
    public async Task Contents_of_an_embedded_resource_can_be_read()
    {
        var reader = GiveMe.The<IEmbeddedResourceReader>();

        var result = reader.ReadAsString("Baked.Test.Resource.EmbeddedResource.txt");
        result.ShouldBe("This is an embedded resource content");

        result = await reader.ReadAsStringAsync("Baked.Test.Resource.EmbeddedResource.txt");
        result.ShouldBe("This is an embedded resource content");
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("NotExistingResource.txt")]
    public async Task Returns_null_when_embedded_resource_does_not_exits(string subpath)
    {
        var reader = GiveMe.The<IEmbeddedResourceReader>();

        var result = reader.ReadAsString(subpath);
        result.ShouldBeNull();

        result = await reader.ReadAsStringAsync(subpath);
        result.ShouldBeNull();
    }
}