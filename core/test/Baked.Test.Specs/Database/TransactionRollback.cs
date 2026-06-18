using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;

namespace Baked.Test.Database;

public class TransactionRollback : TestNfr
{
    [Test]
    public async Task Entity_created_without_a_transaction_does_not_persists_when_an_error_occurs()
    {
        var @string = $"{Guid.NewGuid()}";
        var content = JsonContent.Create(new { @string });
        var response = await Client.PostAsync($"transaction-samples/rollback", content);

        var entitiesResponse = await Client.GetAsync("entities");
        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);

        var entities = JsonConvert.DeserializeObject<IEnumerable<JObject>>(await entitiesResponse.Content.ReadAsStringAsync()) ?? [];
        entities.LastOrDefault()?.Value<string>("string").ShouldNotBe(@string);
    }

    [Test]
    public async Task Entity_created_by_a_transaction_committed_asynchronously_persists_when_an_error_occurs()
    {
        var response = await Client.PostAsync($"transaction-samples/commit-action", null);

        var entitiesContent = await Client.GetAsync("entities");
        dynamic? result = await entitiesContent.Content.Deserialize();

        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);
        ((string?)result?.Last.String)?.ShouldBe("transaction action");
    }

    [Test]
    public async Task Only_the_updates_outside_of_transaction_are_rolled_back_when_an_error_occurs()
    {
        var response = await Client.PostAsync($"transaction-samples/commit-func", null);

        var entitiesContent = await Client.GetAsync("entities");
        dynamic? result = await entitiesContent.Content.Deserialize();

        response.StatusCode.ShouldNotBe(HttpStatusCode.NotFound);
        ((string?)result?.Last.String)?.ShouldBe("transaction func");
    }

    [Test]
    public async Task Multiple_entities_created_within_request_transaction_does_not_persists_when_an_error_occurs()
    {
        var response = await Client.PostAsync("/transaction-samples/rollback-multiple", JsonContent.Create(new { @string = "test" }));
        response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);

        var parentResponse = await Client.GetAsync("/parents");
        dynamic? parents = await parentResponse.Content.Deserialize() ?? throw new("Expected content to be not null");

        ((IEnumerable<dynamic>)parents).Count().ShouldBe(0);

        var childrenResponse = await Client.GetAsync("/children");
        dynamic? children = await childrenResponse.Content.Deserialize() ?? throw new("Expected content to be not null");

        ((IEnumerable<dynamic>)children).Count().ShouldBe(0);
    }
}