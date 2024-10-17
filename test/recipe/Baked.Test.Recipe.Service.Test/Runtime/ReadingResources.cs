using Microsoft.Extensions.FileProviders;

namespace Baked.Test.Runtime;

public class ReadingResources : TestServiceSpec
{
    [Test]
    public async Task Contents_of_a_physical_resource_can_be_read()
    {
        var subPath = "/Core/PhysicalResource.txt";
        var expectedContent = "physical resource content";
        var reader = GiveMe.The<IFileProvider>();

        var result = reader.ReadAsString(subPath);
        result.ShouldBe(expectedContent);

        result = await reader.ReadAsStringAsync(subPath);
        result.ShouldBe(expectedContent);
    }

    [Test]
    public async Task Contents_of_an_embedded_resource_can_be_read()
    {
        var subPath = "/Core/EmbeddedResource.txt";
        var expectedContent = "embedded resource content";
        var reader = GiveMe.The<IFileProvider>();

        var result = reader.ReadAsString(subPath);
        result.ShouldBe(expectedContent);

        result = await reader.ReadAsStringAsync(subPath);
        result.ShouldBe(expectedContent);
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