using Do.Architecture;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace Do.Test.Integration;

public class SingletonServiceTests : TestServiceNfr
{
    protected override Application ForgeApplication() =>
        Forge.New
            .Service(
                business: c => c.Default(),
                database: c => c.InMemory(),
                exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}")
            );

    static (bool, int)[] _testExceptionSuccessCases = [(true, 400), (false, 500)];

    // exception handing
    [Test]
    public async Task Singleton_test_exception([ValueSource(nameof(_testExceptionSuccessCases))] (bool handled, int code) successCase)
    {
        var response = await Client.PostAsync($"singleton/test-exception?handled={successCase.handled}", null);

        var problemDetails = response.Content.ReadFromJsonAsync<ProblemDetails>().Result;

        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(successCase.code);
    }

    [Test]
    public async Task Singleton_test_transaction_action()
    {
        await Client.PostAsync($"singleton/test-transaction-action", null);

        var result = await Client.GetEntities();

        result.ShouldNotBeNull();
        $"{result.Last().GetProperty("stringData")}".ShouldBe("transaction action");
    }

    [Test]
    public async Task Singleton_test_transaction_func()
    {
        await Client.PostAsync($"singleton/test-transaction-func", null);

        var result = await Client.GetEntities();

        result.ShouldNotBeNull();
        $"{result.Last().GetProperty("stringData")}".ShouldBe("rollback");
    }

    [Test]
    public async Task Singleton_test_transaction_nullable()
    {
        var result = await Client.GetEntities();
        var id = $"{result.Last().GetProperty("id")}";

        var response = await Client.PostAsync(
            $"singleton/test-transaction-nullable",
            JsonContent.Create(new { entityId = id })
        );

        response.ShouldNotBeNull();

        result = await Client.GetEntities();

        var entity = result.Where(e => $"{e.GetProperty("id")}" == id).FirstOrDefault();
        $"{entity.GetProperty("stringData")}".ShouldBe("transaction nullable");
    }

    [Test]
    public async Task Singleton_test_async_object()
    {
        var response = await Client.PutAsync(
            "singleton/test-async-object",
            JsonContent.Create(new { testValue = "Test value" })
        );

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        $"{result.GetProperty("testValue")}".ShouldBe("Test value");
    }

    [Test]
    public async Task Singleton_test_object()
    {
        var response = await Client.PutAsync(
            "singleton/test-object",
            JsonContent.Create(new { testValue = "Test value" })
        );

        var result = await response.Content.ReadFromJsonAsync<JsonElement>();
        $"{result.GetProperty("testValue")}".ShouldBe("Test value");
    }
}
