using Microsoft.Extensions.FileProviders;

namespace Baked.Test.Resource;

public class ReadingFromEmbeddedResource : TestServiceSpec
{
    [Test]
    public async Task Contents_of_a_resource_can_be_read()
    {
        var reader = GiveMe.The<IFileProvider>();

        var result = reader.ReadAsString("Baked.Test.Resource.EmbeddedResource.txt");
        result.ShouldBe("embedded resource content");

        result = await reader.ReadAsStringAsync("Baked.Test.Resource.EmbeddedResource.txt");
        result.ShouldBe("embedded resource content");
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("NotExistingResource.txt")]
    public async Task Returns_null_when_embedded_resource_does_not_exits(string subpath)
    {
        var reader = GiveMe.The<IFileProvider>();

        var result = reader.ReadAsString(subpath);
        result.ShouldBeNull();

        result = await reader.ReadAsStringAsync(subpath);
        result.ShouldBeNull();
    }
}