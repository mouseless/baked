namespace Baked.Test.CodingStyle;

public class SerializingOnlyIdAndLabelsForParents : TestNfr
{
    [Test]
    public async Task Serializes_only_id_and_label_for_direct_parent()
    {
        var parent = await Client.PostParents(name: "parent");
        await Client.PostParentsChildren(id: (object)parent.id);

        var children = await Client.GetParentsChildren((object)parent.id);
        object? actual = children[0].parent;

        actual?.ShouldDeeplyBe(new { parent?.id, name = "parent" });
    }

    [Test]
    public async Task Does_not_skip_properties_when_parent_child_relation_is_broken()
    {
        var parent = await Client.PostParents(name: "parent", surname: "wrapper");
        await Client.PostParentsChildren(id: (object)parent.id);

        var children = await Client.GetParentsChildren((object)parent.id);
        object? actual = children?[0].parentWrapper.parent;

        actual?.ShouldDeeplyBe(new
        {
            parent?.id,
            name = "parent",
            surname = "wrapper",
            description = (string?)null
        });
    }

    [Test]
    [Ignore("not implemented")]
    public void Open_api_documentation_ignores_skipped_properties() =>
        Assert.Fail();
}