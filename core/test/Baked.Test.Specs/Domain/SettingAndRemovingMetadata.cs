using Baked.Domain.Model;

namespace Baked.Test.Domain;

public class SettingAndRemovingMetadata : TestSpec
{
    public class CustomAttribute : Attribute;

    [Test]
    public void Uses_a_mutable_interface_to_set_an_attribute()
    {
        var attributes = GiveMe.AnAttributeCollection();

        ((IMutableAttributeCollection)attributes).Set(new CustomAttribute());

        attributes.Contains<CustomAttribute>().ShouldBeTrue();
    }

    [Test]
    public void Uses_a_mutable_interface_to_remove_attribute()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new CustomAttribute());

        ((IMutableAttributeCollection)attributes).Remove<CustomAttribute>();

        attributes.Contains<CustomAttribute>().ShouldBeFalse();
    }
}