using System.Net.Http.Json;

namespace Baked.Test.Core;

public class ReadingResources : TestServiceNfr
{
    [TestCase("/Core/ApplicationPhysical.txt", "application physical")]
    [TestCase("/Core/ApplicationEmbedded.txt", "application embedded")]
    [TestCase("/Core/DomainEmbedded.txt", "domain embedded")]
    public async Task Contents_of_a_resource_can_be_read(string subPath, string expected)
    {
        var response = await Client.PostAsync("resource-samples/read", JsonContent.Create(new { subPath }));
        var content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBe(expected);

        response = await Client.PostAsync("resource-samples/read-async", JsonContent.Create(new { subPath }));
        content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBe(expected);
    }

    [Test]
    public async Task Returns_empty_string_when_embedded_resource_does_not_exits()
    {
        var response = await Client.PostAsync("resource-samples/read", JsonContent.Create(new { subPath = "NotExistingResource.txt" }));
        var content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBeEmpty();

        response = await Client.PostAsync("resource-samples/read-async", JsonContent.Create(new { subPath = "NotExistingResource.txt" }));
        content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBeEmpty();
    }
}