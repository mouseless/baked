using System.Net.Http.Json;

namespace Baked.Test.Core;

public class ReadingFiles : TestNfr
{
    [TestCase("/Core/ApplicationPhysical.txt", "application physical")]
    [TestCase("/Core/ApplicationEmbedded.txt", "application embedded")]
    [TestCase("/Core/DomainEmbedded.txt", "domain embedded")]
    public async Task Contents_of_a_file_can_be_read(string subPath, string expected)
    {
        var response = await Client.PostAsync("file-samples/read", JsonContent.Create(new { subPath }));
        var content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBe(expected);

        response = await Client.PostAsync("file-samples/read-async", JsonContent.Create(new { subPath }));
        content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBe(expected);
    }

    [Test]
    public async Task Returns_empty_string_when_embedded_file_does_not_exist()
    {
        var response = await Client.PostAsync("file-samples/read", JsonContent.Create(new { subPath = "NotExistingFile.txt" }));
        var content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBeEmpty();

        response = await Client.PostAsync("file-samples/read-async", JsonContent.Create(new { subPath = "NotExistingFile.txt" }));
        content = await response.Content.ReadFromJsonAsync<string>();

        content.ShouldBeEmpty();
    }
}