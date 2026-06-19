using Baked.Business;
using Baked.Playground.Orm;

namespace Baked.Test.CodingStyle;

public class LabelProperties : TestSpec
{
    [Test]
    public void Properties_are_marked_as_label_depending_on_their_name()
    {
        var child = GiveMe.TheTypeModel<Child>().GetMembers();
        var name = child.Properties[nameof(Child.Name)];

        name.Has<LabelAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Non_public_properties_are_ignored()
    {
        var child = GiveMe.TheTypeModel<Child>().GetMembers();
        var title = child.Properties["Title"];

        title.Has<LabelAttribute>().ShouldBeFalse();
    }
}