namespace Baked.Test.CodingStyle;

public class SerializingOnlyIdAndLabelsForParents : TestNfr
{
    [Test]
    public async Task Serializes_only_id_and_label_for_direct_parent_of_an_entity()
    {
        var parent = await Client.PostParents(name: "parent", surname: "test");
        await Client.PostParentsChildren(id: (object)parent.id);

        var children = await Client.GetParentsChildren((object)parent.id);
        object? actual = children[0].parent;

        actual?.ShouldDeeplyBe(new { parent?.id, name = "parent", surname = "test" });
    }

    [Test]
    public async Task Does_not_skip_properties_when_parent_child_relation_is_broken_for_an_entity()
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
    public async Task Serializes_only_id_and_label_for_direct_parent_of_a_rich_transient()
    {
        var response = await Client.GetAsync("/rich-transient-with-children/1");
        dynamic? child = await response.Content.Deserialize();
        object? actual = child?.parent;

        actual?.ShouldDeeplyBe(new { id = "1", name = "1 parent" });
    }

    [Test]
    public async Task Does_not_skip_properties_when_parent_child_relation_is_broken_for_a_rich_transient()
    {
        var response = await Client.GetAsync("/rich-transient-with-children/1");
        dynamic? child = await response.Content.Deserialize();
        object? actual = child?.parentWrapper;

        actual?.ShouldDeeplyBe(new
        {
            id = "1",
            name = "1 parent",
            description = "1 parent description"
        });
    }
}