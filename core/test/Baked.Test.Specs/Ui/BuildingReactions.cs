using Baked.Ui;

namespace Baked.Test.Ui;

public class BuildingReactions : TestSpec
{
    [Test]
    public void Reactions_is_null_by_default()
    {
        var descriptor = new Text().ToDescriptor();

        descriptor.Reactions.ShouldBeNull();
    }

    [Test]
    public void Show_reaction_is_set_to_show_key()
    {
        var descriptor = new Text().ToDescriptor();

        descriptor.ShowOn(GiveMe.AString());

        descriptor.Reactions.ShouldNotBeNull();
        descriptor.Reactions.ContainsKey("show");
    }

    [Test]
    public void Reload_reaction_is_set_to_when_key()
    {
        var descriptor = new Text().ToDescriptor();

        descriptor.ReloadOn(GiveMe.AString());

        descriptor.Reactions.ShouldNotBeNull();
        descriptor.Reactions.ContainsKey("reload");
    }

    [Test]
    public void First_trigger_is_set_to_reactions_directly()
    {
        var descriptor = new Text().ToDescriptor();

        descriptor.ReloadOn(GiveMe.AString());

        descriptor.Reactions.ShouldNotBeNull();
        descriptor.Reactions["reload"].ShouldBeOfType<OnTrigger>();
    }

    [Test]
    public void Trigger_has_no_constraint_by_default()
    {
        var descriptor = new Text().ToDescriptor();

        descriptor.ShowOn(GiveMe.AString());

        descriptor.Reactions.ShouldNotBeNull();
        descriptor.Reactions["show"].ShouldBeOfType<OnTrigger>().Constraint.ShouldBeNull();
    }

    [Test]
    public void Trigger_keeps_given_constraint()
    {
        var descriptor = new Text().ToDescriptor();
        var value = GiveMe.AString();

        descriptor.ShowWhen(GiveMe.AString(), constraint: Evaluate.Is(value));

        descriptor.Reactions.ShouldNotBeNull();
        var constraint = descriptor.Reactions["show"].ShouldBeOfType<WhenTrigger>()
            .Constraint.ShouldBeOfType<IsConstraint>();
        constraint.Type.ShouldBe("Is");
        constraint.Is.ShouldBe(value);
        constraint.Null.ShouldBeNull();
    }

    [Test]
    public void Constraint_is_null_constraint_when_its_value_is_null()
    {
        var constraint = Evaluate.Is(null);

        constraint.Is.ShouldBeNull();
        constraint.Null.ShouldBe(true);
    }

    [Test]
    public void Not_constraint_is_built_with_is_not_value()
    {
        var value = GiveMe.AString();

        var constraint = Evaluate.IsNot(value);

        constraint.Type.ShouldBe("IsNot");
        constraint.IsNot.ShouldBe(value);
        constraint.Null.ShouldBeNull();
    }

    [Test]
    public void When_further_triggers_are_added__root_trigger_is_converted_to_composite()
    {
        var descriptor = new Text().ToDescriptor();

        descriptor.ReloadOn(GiveMe.AString());
        descriptor.ReloadWhen(GiveMe.AString());
        descriptor.ReloadWhen(GiveMe.AString());

        descriptor.Reactions.ShouldNotBeNull();
        var composite = descriptor.Reactions["reload"].ShouldBeOfType<CompositeTrigger>();
        composite.Parts.Count.ShouldBe(3);
        composite.Parts[0].ShouldBeOfType<OnTrigger>();
        composite.Parts[1].ShouldBeOfType<WhenTrigger>();
        composite.Parts[2].ShouldBeOfType<WhenTrigger>();
    }
}