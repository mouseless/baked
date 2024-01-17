using Do.Architecture;
using System.Net.Http.Json;
using System.Text.Json;

namespace Do.Test.Database;

public class TransactionRollback : TestServiceNfr
{
    protected override Application ForgeApplication() =>
        Forge.New
            .Service(
                business: c => c.Default(),
                database: c => c.Sqlite("Do.Test.Blueprits.Service.Test")
            );

    [Test]
    public async Task Entity_created_by_a_transaction_committed_asynchronously_persists_when_an_error_occurs()
    {
        await Client.PostAsync($"singleton/test-transaction-action", null);

        var entitiesContent = await Client.GetAsync("entities");
        var result = await entitiesContent.Content.ReadFromJsonAsync<List<JsonElement>>() ?? throw new("No entities in database");

        result.ShouldNotBeNull();
        var stringData = $"{result.Last().GetProperty("stringData")}";
        stringData.ShouldBe("transaction action");
    }

    [Test]
    public async Task Only_the_updates_outside_of_transaction_are_rolled_back_when_an_error_occurs()
    {
        await Client.PostAsync($"singleton/test-transaction-func", null);

        var entitiesContent = await Client.GetAsync("entities");
        var result = await entitiesContent.Content.ReadFromJsonAsync<List<JsonElement>>() ?? throw new("No entities in database");

        result.ShouldNotBeNull();
        $"{result.Last().GetProperty("stringData")}".ShouldBe("transaction func");
    }
}
