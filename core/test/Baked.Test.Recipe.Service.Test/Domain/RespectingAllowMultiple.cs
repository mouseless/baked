using Baked.Domain.Model;

namespace Baked.Test.Domain;

public class RespectingAllowMultiple : TestServiceSpec
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MultipleAttribute : Attribute;
    public class SingleAttribute : Attribute;

    [Test]
    public void When_given_attribute_type_does_not_allow_multiple__it_removes_old_if_any__leaving_single_value()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new SingleAttribute());
        var lastSingle = new SingleAttribute();

        ((IMutableAttributeCollection)attributes).Set(lastSingle);

        attributes.Get<SingleAttribute>().ShouldBe(lastSingle);
    }

    [Test]
    public void Add_throws_invalid_operation_when_a_single_attribute_is_given()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => ((IMutableAttributeCollection)attributes).Add(new SingleAttribute());

        action.ShouldThrow<InvalidOperationException>();
    }

    [Test]
    public void When_given_attribute_type_allows_multiple__multiple_instances_of_same_type_is_added()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new MultipleAttribute());

        ((IMutableAttributeCollection)attributes).Add(new MultipleAttribute());

        attributes.GetAll<MultipleAttribute>().Count().ShouldBe(2);
    }

    [Test]
    public void Set_throws_invalid_operation_when_a_multiple_attribute_is_given()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => ((IMutableAttributeCollection)attributes).Set(new MultipleAttribute());

        action.ShouldThrow<InvalidOperationException>();
    }

    [Test]
    public void Removing_clears_attributes_that_allow_multiple()
    {
        var attributes = GiveMe.AnAttributeCollection(items: [new MultipleAttribute(), new MultipleAttribute()]);

        ((IMutableAttributeCollection)attributes).Remove<MultipleAttribute>();

        attributes.TryGetAll<MultipleAttribute>(out var result);
        result.ShouldBeNull();
    }
}