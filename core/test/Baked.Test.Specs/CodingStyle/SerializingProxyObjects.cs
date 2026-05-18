namespace Baked.Test.CodingStyle;

public class SerializingProxyObjects : TestNfr
{
    [TestCase]
    public async Task Fetch_parents_false_returns_only_id_props_filled()
    {
        var parent = await Client.PostParents(name: "parent", surname: "test");
        await Client.PostParentsChildren(id: (object)parent.id);

        var response = await Client.GetAsync("/children/first?fetchParents=false");
        dynamic? child = await response.Content.Deserialize();

        object? childParent = child?.parent;
        childParent.ShouldNotBeNull();
        childParent.ShouldDeeplyBe(new Dictionary<string, object?>()
        {
            { "id", parent.id }
        });

        object? wrappedParent = child?.parentWrapper.parent;
        wrappedParent.ShouldNotBeNull();
        wrappedParent.ShouldDeeplyBe(new Dictionary<string, object?>()
        {
            { "id", parent.id },
            { "name", default },
            { "description", default },
            { "status", default },
            { "role", default },
            { "surname", default },
        });

        object? parentInterface = child?.parentInterface;
        parentInterface.ShouldNotBeNull();
        parentInterface?.ShouldDeeplyBe(new Dictionary<string, object?>()
        {
            { "$type", "Baked.Playground.Orm.Parent, Baked.Playground" },
            { "id", parent.id },
            { "name", default },
            { "description", default },
            { "status", default },
            { "role", default },
            { "surname", default },
        });
    }
}