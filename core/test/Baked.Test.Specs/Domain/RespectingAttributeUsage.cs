using Baked.Domain.Configuration;
using Baked.Domain.Conventions;
using Baked.Domain.Model;
using Baked.Playground.Business;
using Baked.Playground.Orm;

namespace Baked.Test.Domain;

public class RespectingAttributeUsage : TestSpec
{
    [AttributeUsage(AttributeTargets.All)]
    public class TargetAllAttribute : Attribute;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class TargetAllMultipleAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Class)]
    public class TargetClassAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TargetClassMultipleAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Method)]
    public class TargetMethodAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Parameter)]
    public class TargetParameterAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Property)]
    public class TargetPropertyAttribute : Attribute;

    void Add(TypeModel type, Attribute attribue)
    {
        var domain = GiveMe.TheDomainModel();
        var convention = new AddAttributeConvention<TypeModelContext>(
            _when: _ => true,
            _apply: (c, set) => set(c.Type.GetMetadata(), attribue)
        );
        convention.Apply(new TypeModelContext()
        {
            Domain = domain,
            Type = type
        });
    }

    void Set(TypeModel type, Attribute attribue)
    {
        var domain = GiveMe.TheDomainModel();
        var convention = new SetAttributeConvention<TypeModelContext>(
            _when: _ => true,
            _apply: (c, set) => set(c.Type.GetMetadata(), attribue)
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

        var addAll = () => Add(type, new TargetAllMultipleAttribute());
        var addClass = () => Add(type, new TargetClassMultipleAttribute());
        var addMethod = () => Add(type, new TargetMethodAttribute());
        var addParameter = () => Add(type, new TargetParameterAttribute());
        var addProperty = () => Add(type, new TargetPropertyAttribute());

        addAll.ShouldNotThrow();
        addClass.ShouldNotThrow();

        addMethod.ShouldThrow<DiagnosticsException>();
        addParameter.ShouldThrow<DiagnosticsException>();
        addProperty.ShouldThrow<DiagnosticsException>();
    }

    [Test]
    public void Set_convention_respects_attribute_target()
    {
        var domain = GiveMe.TheDomainModel();
        var type = domain.Types[typeof(Parent)].GetMetadata();

        var addAll = () => Set(type, new TargetAllAttribute());
        var addClass = () => Set(type, new TargetClassAttribute());
        var addMethod = () => Set(type, new TargetMethodAttribute());
        var addParameter = () => Set(type, new TargetParameterAttribute());
        var addProperty = () => Set(type, new TargetPropertyAttribute());

        addAll.ShouldNotThrow();
        addClass.ShouldNotThrow();

        addMethod.ShouldThrow<DiagnosticsException>();
        addParameter.ShouldThrow<DiagnosticsException>();
        addProperty.ShouldThrow<DiagnosticsException>();
    }
}