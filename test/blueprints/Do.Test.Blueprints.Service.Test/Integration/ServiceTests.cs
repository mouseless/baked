using Do.Architecture;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace Do.Test.Integration;

public class ServiceTests : IntegrationSpec<ServiceTests>
{
    protected override Application Application =>
        Forge.New
            .Service(
                business: c => c.Default(),
                database: c => c.MySql().ForDevelopment(c.Sqlite()),
                exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
                configure: app => app.Features.AddConfigurationOverrider()
            );

    static (bool, int)[] _testExceptionSuccessCases = [(true, 400), (false, 500)];

    [Test]
    public async Task Singleton_test_exception([ValueSource(nameof(_testExceptionSuccessCases))] (bool handled, int code) successCase)
    {
        var client = Factory.CreateClient();

        var response = await client.PostAsync($"singleton/test-exception?handled={successCase.handled}", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(successCase.code);
    }

    [Test]
    public async Task Singleton_test_transaction_action()
    {
        var client = Factory.CreateClient();

        var response = await client.PostAsync($"singleton/test-transaction-action", null);

        response.ShouldNotBeNull();
        response.Content.ReadFromJsonAsync<ProblemDetails>().Result.ShouldNotBeNull();

        var entitiesResponse = await client.GetAsync("entities");
        entitiesResponse.ShouldNotBeNull();

        var result = await entitiesResponse.Content.ReadFromJsonAsync<List<JsonElement>>();

        result.ShouldNotBeNull();
        $"{result.Last().GetProperty("stringData")}".ShouldBe("transaction action");
    }
}
