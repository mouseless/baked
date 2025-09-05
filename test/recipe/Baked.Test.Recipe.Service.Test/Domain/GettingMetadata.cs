namespace Baked.Test.Domain;

public class GettingMetadata : TestServiceSpec
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MultipleAttribute : Attribute;
    public class SingleAttribute : Attribute;

    [Test]
    public void Get_is_used_to_retrieve_single_attributes()
    {
        var single = new SingleAttribute();
        var attributes = GiveMe.AnAttributeCollection(item: single);

        attributes.Get<SingleAttribute>().ShouldBe(single);
        attributes.Get(typeof(SingleAttribute)).ShouldBe(single);

        attributes.TryGet<SingleAttribute>(out var actual);
        actual.ShouldBe(single);
    }

    [Test]
    public void Get_throws_exception_for_multiple_attributes()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => { attributes.Get<MultipleAttribute>(); };
        action.ShouldThrow<InvalidOperationException>();

        action = () => { attributes.Get(typeof(MultipleAttribute)); };
        action.ShouldThrow<InvalidOperationException>();

        action = () => { attributes.TryGet<MultipleAttribute>(out var _); };
        action.ShouldThrow<InvalidOperationException>();
    }

    [Test]
    public void GetAll_is_used_to_retrieve_multiple_attributes()
    {
        var multiple = new MultipleAttribute();
        var attributes = GiveMe.AnAttributeCollection(item: multiple);

        attributes.GetAll<MultipleAttribute>().ShouldBe([multiple]);
        attributes.GetAll(typeof(MultipleAttribute)).ShouldBe([multiple]);

        attributes.TryGetAll<MultipleAttribute>(out var actual);
        actual.ShouldBe([multiple]);
    }

    [Test]
    public void GetAll_throws_exception_for_single_attributes()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => { attributes.GetAll<SingleAttribute>(); };
        action.ShouldThrow<InvalidOperationException>();

        action = () => { attributes.GetAll(typeof(SingleAttribute)); };
        action.ShouldThrow<InvalidOperationException>();

        action = () => { attributes.TryGetAll<SingleAttribute>(out var _); };
        action.ShouldThrow<InvalidOperationException>();
    }
}