using Do.Test.Business;
using Do.Test.Lifetime;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Test.CodingStyle;

public class RegisteringServicesByConvention : TestServiceSpec
{
    [Test]
    public void Types_containing_a_method_named_with_are_registered_as_transient([Values(typeof(Transient), typeof(TransientAsync))] Type type)
    {
        var actual1 = GiveMe.A(type);
        var actual2 = GiveMe.A(type);

        actual1.ShouldNotBeSameAs(actual2);
    }

    [Test]
    public void Transient_types_with_generic_type_arguments_are_registered([Values(typeof(TransientGeneric<Singleton>))] Type type)
    {
        var actual1 = GiveMe.A(type);
        var actual2 = GiveMe.A(type);

        actual1.ShouldNotBeSameAs(actual2);
    }

    [Test]
    public void Transient_services_have_singleton_factories([Values(typeof(Func<Transient>), typeof(Func<TransientAsync>), typeof(Func<TransientGeneric<Singleton>>))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
    }

    [Test]
    public void Transient_types_with_interfaces_are_registered_as_implementations()
    {
        var actual1 = GiveMe.The<IOperation>();
        var actual2 = GiveMe.The<IOperation>();

        actual1.ShouldNotBeSameAs(actual2);
        actual1.GetType().ShouldBe(typeof(Operation));
    }

    [Test]
    public void Types_that_has_Context_suffix_are_registered_as_scoped()
    {
        var actual1 = GiveMe.The<ScopedContext>();
        var actual2 = GiveMe.The<ScopedContext>();
        var actual3 = GiveMe.The<IServiceProvider>().CreateScope().ServiceProvider.GetRequiredService<ScopedContext>();

        actual1.ShouldBeSameAs(actual2);
        actual1.ShouldNotBeSameAs(actual3);
    }

    [Test]
    public void Scoped_services_have_singleton_factories()
    {
        var actual1 = GiveMe.The<Func<ScopedContext>>();
        var actual2 = GiveMe.The<Func<ScopedContext>>();

        actual1.ShouldBeSameAs(actual2);
    }

    [Test]
    public void Types_without_a_with_method_are_registered_as_singleton([Values(typeof(Singleton), typeof(Class))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
    }

    [Test]
    public void Types_are_not_registered_as_singleton_when_they_have_public_properties([Values(typeof(Data))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void Singleton_types_with_interfaces_are_registered_as_implementations([Values(typeof(IInterface))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
        actual1.GetType().ShouldBe(typeof(Class));
    }

    [Test]
    public void Interfaces_are_only_registered_through_their_implemented_classes([Values(typeof(IInterfaceWithNoImplementation))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }
}