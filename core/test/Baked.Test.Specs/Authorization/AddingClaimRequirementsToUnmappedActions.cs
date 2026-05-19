using System.Net;

namespace Baked.Test.Authorization;

public class AddingClaimRequirementsToUnmappedActions : TestNfr
{
    [Test]
    public async Task Actions_with_no_mapped_method_requires_base_claims()
    {
        Client.DefaultRequestHeaders.Clear();

        var response = await Client.GetAsync($"parents/{Guid.NewGuid()}");

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Test]
    public async Task Respects_allow_anonymous()
    {
        Client.DefaultRequestHeaders.Clear();

        var response = await Client.GetAsync($"children/{Guid.NewGuid()}");

        response.IsSuccessStatusCode.ShouldBeFalse();
        response.StatusCode.ShouldNotBe(HttpStatusCode.Unauthorized);
    }
}