namespace Baked.Test.CodingStyle;

public class SerializingProxyObjects : TestNfr
{
    [TestCase]
    public async Task Fetch_parents_false_returns_only_id_props_filled()
    {
        var response = await Client.GetAsync("/children/first?fetchParents=false");
        dynamic? child = await response.Content.Deserialize();

        object? actual = child?.@interface;

        actual?.ShouldDeeplyBe(new Dictionary<string, object>()
        {
            { "$type", "Baked.Playground.Orm.Parent" }
        });
    }
}