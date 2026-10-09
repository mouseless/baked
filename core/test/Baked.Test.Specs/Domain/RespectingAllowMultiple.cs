using Baked.Buildtime.Diagnostics;
using Baked.Domain.Model;

namespace Baked.Test.Domain;

public class RespectingAllowMultiple : TestSpec
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class Multiple : Attribute;

    [AttributeUsage(AttributeTargets.All)]
    public class Single : Attribute;

    [Test]
    public void When_given_attribute_type_does_not_allow_multiple__it_removes_old_if_any__leaving_single_value()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new Single());
        var lastSingle = new Single();

        ((IMutableAttributeCollection)attributes).Set(lastSingle);

        attributes.Get<Single>().ShouldBe(lastSingle);
    }

    [Test]
    public void Add_throws_invalid_operation_when_a_single_attribute_is_given()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => ((IMutableAttributeCollection)attributes).Add(new Single());

        action.ShouldThrow<DiagnosticException>();
    }

    [Test]
    public void When_given_attribute_type_allows_multiple__multiple_instances_of_same_type_is_added()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new Multiple());

        ((IMutableAttributeCollection)attributes).Add(new Multiple());

        attributes.GetAll<Multiple>().Count().ShouldBe(2);
    }

    [Test]
    public void Set_throws_invalid_operation_when_a_multiple_attribute_is_given()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => ((IMutableAttributeCollection)attributes).Set(new Multiple());

        action.ShouldThrow<DiagnosticException>();
    }

    [Test]
    public void Removing_clears_attributes_that_allow_multiple()
    {
        var attributes = GiveMe.AnAttributeCollection(items: [new Multiple(), new Multiple()]);

        ((IMutableAttributeCollection)attributes).Remove<Multiple>();

        attributes.TryGetAll<Multiple>(out var result);
        result.ShouldBeNull();
    }
}