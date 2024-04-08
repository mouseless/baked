using Do.Test.ExceptionHandling;
using Do.Test.Lifetime;
using Do.Test.Orm;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Test.Business;

public class RegisteringServices : TestServiceSpec
{
    [Test]
    public void Types_containing_a_method_named_with_and_returns_self_are_registered_as_transient([Values(typeof(Entity), typeof(Operation), typeof(TransientAsync))] Type type)
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
    public void Transient_services_have_singleton_factories([Values(typeof(Func<Transient>), typeof(Func<Operation>), typeof(Func<TransientGeneric<Singleton>>))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
    }

    [Test]
    public void Types_without__with__methods_are_registered_as_singleton([Values(typeof(Singleton), typeof(Class))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
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
    public void Static_types_are_not_registered()
    {
        var action = () => GiveMe.The(typeof(Static));

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(Static));
    }

    [Test]
    public void Value_types_are_not_registered([Values(typeof(Struct), typeof(Status))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void Records_are_not_registered()
    {
        var action = () => GiveMe.The<Record>();

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(Record));
    }

    [Test]
    public void Abstract_types_are_not_registered()
    {
        var action = () => GiveMe.The<Abstract>();

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(Abstract));
    }

    [Test]
    public void Exception_types_are_not_registered([Values(typeof(SampleException), typeof(Exception), typeof(TestServiceHandledException))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void System_types_are_not_registered([Values(typeof(int), typeof(string), typeof(Guid), typeof(List<>), typeof(Task<>))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void Attributes_are_not_registered()
    {
        var action = () => GiveMe.The<AuthorizationRequiredAttribute>();

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(AuthorizationRequiredAttribute));
    }

    [Test]
    public void Referenced_interfaces_are_not_registered([Values(typeof(IEquatable<Entity>))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void Types_with_generic_type_parameters_are_not_registered([Values(typeof(TransientGeneric<>))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void Non_public_types_are_not_registered()
    {
        var nonPublicType = Activator.CreateInstance("Do.Test.Blueprints.Service", "Do.Test.Business.Internal")?.GetType() ??
            throw new("`Do.Test.Business.Internal` should have existed");
        var action = () => GiveMe.The(nonPublicType);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(nonPublicType);
    }

    [Test]
    public void Delegate_types_are_not_registered([Values(typeof(TaskDelegate))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void Types_having_no_with_method_but_public_properties_are_not_registered([Values(typeof(ClassDTO))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void Interfaces_are_only_registered_through_their_implemented_classes([Values(typeof(IInterfaceWithNoImplementation))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }

    [Test]
    public void User_defined_enumerable_types_are_not_registered([Values(typeof(CustomList), typeof(CustomDictionary))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }
}