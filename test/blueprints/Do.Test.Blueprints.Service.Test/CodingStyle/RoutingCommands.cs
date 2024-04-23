using System.Net.Http.Json;

namespace Do.Test.CodingStyle;

public class RoutingCommands : TestServiceNfr
{
    [Test]
    [Ignore("not implemented")]
    public async Task Initialization_parameters_come_from_query()
    {
        var response = await Client.PostAsync("/operation?query=q", JsonContent.Create(
            new { body = "b" }
        ));

        var actual = await response.Content.ReadAsStringAsync();

        actual.ShouldBe("q:b");
    }
}