using Baked.Buildtime.Diagnostics;
using Baked.Domain.Configuration;
using Baked.Domain.Conventions;
using Baked.Domain.Model;
using Baked.Playground.Business;
using Baked.Playground.Orm;

namespace Baked.Test.Domain;

public class RespectingAttributeUsage : TestSpec
{
    [AttributeUsage(AttributeTargets.All)]
    public class TargetAll : Attribute;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class TargetAllMultiple : Attribute;

    [AttributeUsage(AttributeTargets.Class)]
    public class TargetClass : Attribute;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TargetClassMultiple : Attribute;

    [AttributeUsage(AttributeTargets.Method)]
    public class TargetMethod : Attribute;

    [AttributeUsage(AttributeTargets.Parameter)]
    public class TargetParameter : Attribute;

    [AttributeUsage(AttributeTargets.Property)]
    public class TargetProperty : Attribute;

    void Add(TypeModel type, Attribute attribute)
    {
        var domain = GiveMe.TheDomainModel();
        var convention = new AddAttributeConvention<TypeModelContext>(
            _when: _ => true,
            _apply: (c, set) => set(c.Type.GetMetadata(), attribute),
            _order: Order.At
        );
        convention.Apply(new TypeModelContext()
        {
            Domain = domain,
            Type = type
        });
    }

    void Set(TypeModel type, Attribute attribute)
    {
        var domain = GiveMe.TheDomainModel();
        var convention = new SetAttributeConvention<TypeModelContext>(
            _when: _ => true,
            _apply: (c, set) => set(c.Type.GetMetadata(), attribute),
            _order: Order.At
        );
        convention.Apply(new TypeModelContext()
        {
            Domain = domain,
            Type = type
        });
    }

    [TestCase(typeof(Class), AttributeTargets.Class)]
    [TestCase(typeof(IInterface), AttributeTargets.Interface)]
    [TestCase(typeof(Enumeration), AttributeTargets.Enum)]
    [TestCase(typeof(Struct), AttributeTargets.Struct)]
    public void Types_have_matching_targets(Type type, AttributeTargets target)
    {
        var domain = GiveMe.TheDomainModel();
        var model = domain.Types[type].GetMembers();

        ((ICustomAttributesModel)model).Target.ShouldBe(target);
    }

    [Test]
    public void Methods_have_Method_target()
    {
        var domain = GiveMe.TheDomainModel();
        var type = domain.Types[typeof(Parent)].GetMembers();
        var method = type.Methods["With"];

        ((ICustomAttributesModel)method).Target.ShouldBe(AttributeTargets.Method);
    }

    [Test]
    public void Parameters_have_parameter_target()
    {
        var domain = GiveMe.TheDomainModel();
        var type = domain.Types[typeof(Parent)].GetMembers();
        var method = type.Methods["With"];
        var overload = method.DefaultOverload;
        var parameter = overload.Parameters.First();

        ((ICustomAttributesModel)parameter).Target.ShouldBe(AttributeTargets.Parameter);
    }

    [Test]
    public void Properties_have_property_target()
    {
        var domain = GiveMe.TheDomainModel();
        var type = domain.Types[typeof(Parent)].GetMembers();
        var property = type.Properties["Id"];

        ((ICustomAttributesModel)property).Target.ShouldBe(AttributeTargets.Property);
    }

    [Test]
    public void Add_convention_respects_attribute_target()
    {
        var domain = GiveMe.TheDomainModel();
        var type = domain.Types[typeof(Parent)].GetMetadata();

        var addAll = () => Add(type, new TargetAllMultiple());
        var addClass = () => Add(type, new TargetClassMultiple());
        var addMethod = () => Add(type, new TargetMethod());
        var addParameter = () => Add(type, new TargetParameter());
        var addProperty = () => Add(type, new TargetProperty());

        addAll.ShouldNotThrow();
        addClass.ShouldNotThrow();

        addMethod.ShouldThrow<DiagnosticException>();
        addParameter.ShouldThrow<DiagnosticException>();
        addProperty.ShouldThrow<DiagnosticException>();
    }

    [Test]
    public void Set_convention_respects_attribute_target()
    {
        var domain = GiveMe.TheDomainModel();
        var type = domain.Types[typeof(Parent)].GetMetadata();

        var addAll = () => Set(type, new TargetAll());
        var addClass = () => Set(type, new TargetClass());
        var addMethod = () => Set(type, new TargetMethod());
        var addParameter = () => Set(type, new TargetParameter());
        var addProperty = () => Set(type, new TargetProperty());

        addAll.ShouldNotThrow();
        addClass.ShouldNotThrow();

        addMethod.ShouldThrow<DiagnosticException>();
        addParameter.ShouldThrow<DiagnosticException>();
        addProperty.ShouldThrow<DiagnosticException>();
    }
}