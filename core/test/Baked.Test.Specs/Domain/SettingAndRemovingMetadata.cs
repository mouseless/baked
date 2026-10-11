using Baked.Domain.Model;

namespace Baked.Test.Domain;

public class SettingAndRemovingMetadata : TestSpec
{
    public class Custom : Attribute;

    [Test]
    public void Uses_a_mutable_interface_to_set_an_attribute()
    {
        var attributes = GiveMe.AnAttributeCollection();

        ((IMutableAttributeCollection)attributes).Set(new Custom());

        attributes.Contains<Custom>().ShouldBeTrue();
    }

    [Test]
    public void Uses_a_mutable_interface_to_remove_attribute()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new Custom());

        ((IMutableAttributeCollection)attributes).Remove<Custom>();

        attributes.Contains<Custom>().ShouldBeFalse();
    }
}