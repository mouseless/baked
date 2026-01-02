using Baked.Playground.CodingStyle.EntitySubclassViaComposition;
using Baked.Playground.Orm;
using Baked.Testing;
using Humanizer;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Baked.Test;

public abstract class TestNfr : MonolithNfr
{
    static TestNfr() =>
        Init<Program>();

    readonly IEnumerable<string> _entityNamesToClear = [nameof(Entity), nameof(Parent), nameof(TypedEntity)];

    public override async Task OneTimeTearDown()
    {
        await base.OneTimeTearDown();

        Client.DefaultRequestHeaders.Authorization = UserFixedBearerToken;
        foreach (var entityName in _entityNamesToClear)
        {
            var entitiesRoute = entityName.Kebaberize().Pluralize();
            var entitiesResponse = await Client.GetAsync($"/{entitiesRoute}");
            await CheckResponse($"GET /{entitiesRoute}", entitiesResponse);

            var entities = (IEnumerable?)JsonConvert.DeserializeObject(await entitiesResponse.Content.ReadAsStringAsync()) ?? Array.Empty<object>();
            foreach (dynamic entity in entities)
            {
                var deleteResponse = await Client.DeleteAsync($"/{entitiesRoute}/{entity?.id}");
                await CheckResponse($"DELETE /{entitiesRoute}/{entity?.id}", deleteResponse);
            }
        }
    }

    async Task CheckResponse(string route, HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) { return; }

        throw new($"'{route}' didn't work as expected: {response.StatusCode}\n{await response.Content.ReadAsStringAsync()}");
    }

    public override void SetUp()
    {
        base.SetUp();

        Client.DefaultRequestHeaders.Authorization = UserFixedBearerToken;
    }

    public override void TearDown()
    {
        base.TearDown();

        Client.DefaultRequestHeaders.Clear();
    }

    protected AuthenticationHeaderValue UserFixedBearerToken => GetFixedBearerToken("Jane");
    protected AuthenticationHeaderValue AdminFixedBearerToken => GetFixedBearerToken("John");

    protected AuthenticationHeaderValue GetFixedBearerToken(string name) =>
        AuthenticationHeaderValue.Parse(Configuration.GetRequiredValue<string>($"Authentication:FixedBearerToken:{name}"));
}