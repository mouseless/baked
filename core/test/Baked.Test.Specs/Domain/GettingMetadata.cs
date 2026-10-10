using Baked.Buildtime.Diagnostics;

namespace Baked.Test.Domain;

public class GettingMetadata : TestSpec
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class Multiple : Attribute;

    [AttributeUsage(AttributeTargets.All)]
    public class Single : Attribute;

    [Test]
    public void Get_is_used_to_retrieve_single_attributes()
    {
        var single = new Single();
        var attributes = GiveMe.AnAttributeCollection(item: single);

        attributes.Get<Single>().ShouldBe(single);
        attributes.Get(typeof(Single)).ShouldBe(single);
        attributes.TryGet<Single>(out var actual1).ShouldBeTrue();
        attributes.TryGet(typeof(Single), out var actual2).ShouldBeTrue();
        actual1.ShouldBe(single);
        actual2.ShouldBe(single);
    }

    [Test]
    public void TryGet_returns_false_when_not_found()
    {
        var attributes = GiveMe.AnAttributeCollection();

        attributes.TryGet<Single>(out var _).ShouldBeFalse();
        attributes.TryGet(typeof(Single), out var _).ShouldBeFalse();
    }

    [Test]
    public void Get_throws_exception_for_multiple_attributes()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => { attributes.Get<Multiple>(); };
        action.ShouldThrow<DiagnosticException>();

        action = () => { attributes.Get(typeof(Multiple)); };
        action.ShouldThrow<DiagnosticException>();

        action = () => { attributes.TryGet<Multiple>(out var _); };
        action.ShouldThrow<DiagnosticException>();

        action = () => { attributes.TryGet(typeof(Multiple), out var _); };
        action.ShouldThrow<DiagnosticException>();
    }

    [Test]
    public void GetAll_is_used_to_retrieve_multiple_attributes()
    {
        var multiple = new Multiple();
        var attributes = GiveMe.AnAttributeCollection(item: multiple);

        attributes.GetAll<Multiple>().ShouldBe([multiple]);
        attributes.GetAll(typeof(Multiple)).ShouldBe([multiple]);
        attributes.TryGetAll<Multiple>(out var actual1).ShouldBeTrue();
        attributes.TryGetAll(typeof(Multiple), out var actual2).ShouldBeTrue();
        actual1.ShouldBe([multiple]);
        actual2.ShouldBe([multiple]);
    }

    [Test]
    public void TryGetAll_returns_false_when_not_found()
    {
        var attributes = GiveMe.AnAttributeCollection();

        attributes.TryGetAll<Multiple>(out var _).ShouldBeFalse();
        attributes.TryGetAll(typeof(Multiple), out var _).ShouldBeFalse();
    }

    [Test]
    public void GetAll_throws_exception_for_single_attributes()
    {
        var attributes = GiveMe.AnAttributeCollection();

        var action = () => { attributes.GetAll<Single>(); };
        action.ShouldThrow<DiagnosticException>();

        action = () => { attributes.GetAll(typeof(Single)); };
        action.ShouldThrow<DiagnosticException>();

        action = () => { attributes.TryGetAll<Single>(out var _); };
        action.ShouldThrow<DiagnosticException>();

        action = () => { attributes.TryGetAll(typeof(Single), out var _); };
        action.ShouldThrow<DiagnosticException>();
    }
}