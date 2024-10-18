using System.Net.Http.Json;

namespace Baked.Test.Runtime;

public class ReadingResources : TestServiceNfr
{
    [TestCase("/Core/PhysicalResource.txt", "\"physical resource content\"")]
    [TestCase("/Core/EmbeddedResource.txt", "\"embedded resource content\"")]
    [TestCase("/Core/ApplicationEmbedded.txt", "\"application embedded\"")]
    public async Task Contents_of_a_resource_can_be_read(string subPath, string expected)
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("BaseClaims");

        var response = await Client.PostAsync("resource-samples/read", JsonContent.Create(new { subPath }));
        var content = await response.Content.ReadAsStringAsync();

        content.ShouldBe(expected);

        response = await Client.PostAsync("resource-samples/read-async", JsonContent.Create(new { subPath }));
        content = await response.Content.ReadAsStringAsync();

        content.ShouldBe(expected);
    }

    public async Task Returns_null_when_embedded_resource_does_not_exits()
    {
        Client.DefaultRequestHeaders.Authorization = GetFixedBearerToken("BaseClaims");

        var response = await Client.PostAsync("resource-samples/read", JsonContent.Create(new { subPath = "NotExistingResource.txt" }));
        var content = await response.Content.ReadAsStringAsync();

        content.ShouldBeNull();

        response = await Client.PostAsync("resource-samples/read-async", JsonContent.Create(new { subPath = "NotExistingResource.txt" }));
        content = await response.Content.ReadAsStringAsync();

        content.ShouldBeNull();
    }
}