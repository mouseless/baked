using Microsoft.Extensions.DependencyInjection;

namespace Do.Test.Business;

public class RegisteringServices : TestServiceSpec
{
    [Test]
    public void Types_containing_a_method_named_with_and_returns_self_are_registered_as_transient([Values(typeof(Entity), typeof(OperationObject))] Type type)
    {
        var actual = ApplicationContext.GetServiceDescriptor(type);

        actual.ShouldNotBeNull();
        actual.Lifetime.ShouldBe(ServiceLifetime.Transient);
    }

    [Test]
    public void Transient_services_have_singleton_factories([Values(typeof(Func<Entity>), typeof(Func<OperationObject>))] Type type)
    {
        var actual = ApplicationContext.GetServiceDescriptor(type);

        actual.ShouldNotBeNull();
        actual.Lifetime.ShouldBe(ServiceLifetime.Singleton);
    }

    [Test]
    public void Types_with_IQueryContext_dependencies_are_registered_as_singleton()
    {
        var actual = ApplicationContext.GetServiceDescriptor<Entities>();

        actual.ShouldNotBeNull();
        actual.Lifetime.ShouldBe(ServiceLifetime.Singleton);
    }

    [Test]
    public void Types_with_no__virtual_methods__public_properties__are_registered_as_singleton()
    {
        var actual = ApplicationContext.GetServiceDescriptor<Singleton>();

        actual.ShouldNotBeNull();
        actual.Lifetime.ShouldBe(ServiceLifetime.Singleton);
    }

    [Test]
    public void Value_types_are_not_registered([Values(typeof(Struct), typeof(Status))] Type type)
    {
        var actual = ApplicationContext.GetServiceDescriptor(type);

        actual.ShouldBeNull();
    }

    [Test]
    public void Record_dtos_are_not_registered()
    {
        var actual = ApplicationContext.GetServiceDescriptor(typeof(Record));

        actual.ShouldBeNull();
    }

    [Test]
    public void Abstract_types_are_not_registered()
    {
        var actual = ApplicationContext.GetServiceDescriptor(typeof(ServiceBase));

        actual.ShouldBeNull();
    }
}
