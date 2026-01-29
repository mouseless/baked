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
    public async Task Does_not_trim_props_when_parent_child_relation_is_broken()
    {
        var parent = await Client.PostParents(name: "parent", surname: "wrapper");
        await Client.PostParentsChildren(id: (object)parent.id);

        var children = await Client.GetParentsChildren((object)parent.id);
        object? actual = children?[0].parentWrapper.parent;

        actual?.ShouldDeeplyBe(new
        {
            parent?.id,
            name = "parent",
            surname = "wrapper"
        });
    }
}