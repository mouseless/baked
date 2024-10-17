using Microsoft.Extensions.FileProviders;

namespace Baked.Test.Runtime;

public class ReadingResources : TestServiceSpec
{
    [Test]
    public async Task Contents_of_a_physical_resource_can_be_read()
    {
        var reader = GiveMe.The<IFileProvider>();

        var result = reader.ReadAsString("/Core/PhysicalResource.txt");
        result.ShouldBe("physical resource content");

        result = await reader.ReadAsStringAsync("/Core/PhysicalResource.txt");
        result.ShouldBe("physical resource content");
    }

    [Test]
    public async Task Contents_of_an_embedded_resource_can_be_read()
    {
        var reader = GiveMe.The<IFileProvider>();

        var result = reader.ReadAsString("/Core/EmbeddedResource.txt");
        result.ShouldBe("embedded resource content");

        result = await reader.ReadAsStringAsync("/Core/EmbeddedResource.txt");
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