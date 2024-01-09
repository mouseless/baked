using Microsoft.Extensions.DependencyInjection;

namespace Do.Test.Business;

public class RegisteringServices : TestServiceSpec
{
    [Test]
    public void Types_containing_a_method_named_with_and_returns_self_are_registered_as_transient([Values(typeof(Entity), typeof(OperationObject))] Type type)
    {
        var entityDescriptor = ApplicationContext.GetServiceDescriptor(type);

        entityDescriptor.ShouldNotBeNull();
        entityDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
    }

    [Test]
    public void Transient_services_have_singleton_factories([Values(typeof(Entity), typeof(OperationObject))] Type type)
    {
        var entityFactoryDescriptor = ApplicationContext.GetServiceDescriptor(type);

        entityFactoryDescriptor.ShouldNotBeNull();
        entityFactoryDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
    }

    [Test]
    public void Types_with_IQueryContext_dependencies_are_registered_as_singleton()
    {
        var entitesDescriptor = ApplicationContext.GetServiceDescriptor<Entities>();

        entitesDescriptor.ShouldNotBeNull();
        entitesDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
    }

    [Test]
    public void Types_with_no__virtual_methods__public_properties__are_registered_as_singleton()
    {
        var singletonDescriptor = ApplicationContext.GetServiceDescriptor<Singleton>();

        singletonDescriptor.ShouldNotBeNull();
        singletonDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
    }

    [Test]
    public void Value_types_are_not_registered([Values(typeof(Struct), typeof(Status))] Type type)
    {
        var valueTypeDescriptor = ApplicationContext.GetServiceDescriptor(type);

        valueTypeDescriptor.ShouldBeNull();
    }

    [Test]
    public void Record_dtos_are_not_registered()
    {
        var valueTypeDescriptor = ApplicationContext.GetServiceDescriptor(typeof(Record));

        valueTypeDescriptor.ShouldBeNull();
    }
}
