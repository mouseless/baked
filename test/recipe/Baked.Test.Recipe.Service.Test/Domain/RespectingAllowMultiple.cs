using Baked.Domain.Model;

namespace Baked.Test.Domain;

public class RespectingAllowMultiple : TestServiceSpec
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SingleAttribute : Attribute;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MultipleAttribute : Attribute;

    [Test]
    public void When_given_attribute_type_does_not_allow_multiple__it_removes_old_if_any__leaving_single_value()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new SingleAttribute());
        var lastSingle = new SingleAttribute();

        ((IMutableAttributeCollection)attributes).Add(lastSingle);

        attributes.Get<SingleAttribute>().Count().ShouldBe(1);
        attributes.Get<SingleAttribute>().First().ShouldBe(lastSingle);
    }

    [Test]
    public void When_given_attribute_type_allows_multiple__multiple_instances_of_same_type_is_added()
    {
        var attributes = GiveMe.AnAttributeCollection(item: new MultipleAttribute());

        ((IMutableAttributeCollection)attributes).Add(new MultipleAttribute());

        attributes.Get<MultipleAttribute>().Count().ShouldBe(2);
    }

    [Test]
    public void Removing_clears_when_for_attributes_that_allow_multiple()
    {
        var attributes = GiveMe.AnAttributeCollection(items: [new MultipleAttribute(), new MultipleAttribute()]);

        ((IMutableAttributeCollection)attributes).Remove<MultipleAttribute>();

        attributes.TryGet<MultipleAttribute>(out var result);
        result.ShouldBeNull();
    }
}