using Do.Architecture;
using Do.Database;
using System.Net.Http.Json;

namespace Do.Test.Database;

public class TransactionRollback : TestServiceNfr
{
    protected override string EnvironmentName => "Development";
    protected override Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>>? Database =>
        c => c.Sqlite();

    [Test]
    public async Task Entity_created_without_a_transaction_does_not_persists_when_an_error_occurs()
    {
        var @string = $"{Guid.NewGuid()}";
        var content = JsonContent.Create(new { @string });
        var response = await Client.PostAsync($"transaction/rollback", content);

        var entitiesContent = await Client.GetAsync("entities");
        dynamic? result = await entitiesContent.Content.Deserialize();

        response.StatusCode.ShouldNotBe(System.Net.HttpStatusCode.NotFound);
        ((string?)result?.Last.String)?.ShouldNotBe($"{@string}");
    }

    [Test]
    public async Task Entity_created_by_a_transaction_committed_asynchronously_persists_when_an_error_occurs()
    {
        var response = await Client.PostAsync($"transaction/commit-action", null);

        var entitiesContent = await Client.GetAsync("entities");
        dynamic? result = await entitiesContent.Content.Deserialize();

        response.StatusCode.ShouldNotBe(System.Net.HttpStatusCode.NotFound);
        ((string?)result?.Last.String)?.ShouldBe("transaction action");
    }

    [Test]
    public async Task Only_the_updates_outside_of_transaction_are_rolled_back_when_an_error_occurs()
    {
        var response = await Client.PostAsync($"transaction/commit-func", null);

        var entitiesContent = await Client.GetAsync("entities");
        dynamic? result = await entitiesContent.Content.Deserialize();

        response.StatusCode.ShouldNotBe(System.Net.HttpStatusCode.NotFound);
        ((string?)result?.Last.String)?.ShouldBe("transaction func");
    }
}
