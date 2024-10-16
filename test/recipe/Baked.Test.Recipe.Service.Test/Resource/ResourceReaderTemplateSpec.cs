using Baked.Resource;

namespace Baked.Test.Resource;

public abstract class ResourceReaderTemplateSpec<T> : TestServiceSpec
    where T : ResourceReaderBase
{
    protected abstract string ResourcePath { get; }
    protected abstract string ExpectedContent { get; }

    [Test]
    public async Task Contents_of_a_resource_can_be_read()
    {
        var reader = GiveMe.The<T>();

        var result = reader.ReadAsString(ResourcePath);
        result.ShouldBe(ExpectedContent);

        result = await reader.ReadAsStringAsync(ResourcePath);
        result.ShouldBe(ExpectedContent);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("NotExistingResource.txt")]
    public async Task Returns_null_when_embedded_resource_does_not_exits(string subpath)
    {
        var reader = GiveMe.The<T>();

        var result = reader.ReadAsString(subpath);
        result.ShouldBeNull();

        result = await reader.ReadAsStringAsync(subpath);
        result.ShouldBeNull();
    }
}