using Do.Architecture;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace Do.Test.Integration;

public class SingletonServiceTests : IntegrationSpec<SingletonServiceTests>
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

    [Test]
    public async Task Singleton_test_transaction_func()
    {
        var client = Factory.CreateClient();

        var response = await client.PostAsync($"singleton/test-transaction-func", null);

        response.ShouldNotBeNull();
        response.Content.ReadFromJsonAsync<ProblemDetails>().Result.ShouldNotBeNull();

        var entitiesResponse = await client.GetAsync("entities");
        entitiesResponse.ShouldNotBeNull();

        var result = await entitiesResponse.Content.ReadFromJsonAsync<List<JsonElement>>();

        result.ShouldNotBeNull();
        $"{result.Last().GetProperty("stringData")}".ShouldBe("rollback");
    }

    [Test]
    public async Task Singleton_test_transaction_nullable()
    {
        var client = Factory.CreateClient();

        var entitiesResponse = await client.GetAsync("entities");
        entitiesResponse.ShouldNotBeNull();
        var result = await entitiesResponse.Content.ReadFromJsonAsync<List<JsonElement>>();
        result.ShouldNotBeNull();
        var id = $"{result.Last().GetProperty("id")}";

        var content = JsonContent.Create(new { entityId = id });

        var response = await client.PostAsync($"singleton/test-transaction-nullable", content);

        response.ShouldNotBeNull();

        entitiesResponse = await client.GetAsync("entities");
        entitiesResponse.ShouldNotBeNull();

        result = await entitiesResponse.Content.ReadFromJsonAsync<List<JsonElement>>();

        result.ShouldNotBeNull();
        var entity = result.Where(e => $"{e.GetProperty("id")}" == id).FirstOrDefault();
        $"{entity.GetProperty("stringData")}".ShouldBe("transaction nullable");
    }

    [Test]
    public async Task Singleton_test_async_object()
    {
        var client = Factory.CreateClient();

        var content = JsonContent.Create(new { testValue = "Test value" });
        var response = await client.PutAsync("singleton/test-async-object", content);

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        $"{result.GetProperty("testValue")}".ShouldBe("Test value");
    }

    [Test]
    public async Task Singleton_test_object()
    {
        var client = Factory.CreateClient();

        var content = JsonContent.Create(new { testValue = "Test value" });
        var response = await client.PutAsync("singleton/test-object", content);

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        $"{result.GetProperty("testValue")}".ShouldBe("Test value");
    }
}
