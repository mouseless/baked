using Do.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Test.Business;
public class RegisteringServices : TestServiceSpec
{
    [Test]
    public void Types_containing_a_method_named_with_and_returns_self_are_registered_as_transient([Values(typeof(Entity), typeof(OperationObject))] Type type)
    {
        var actual1 = GiveMe.A(type);
        var actual2 = GiveMe.A(type);

        actual1.ShouldNotBeSameAs(actual2);
    }

    [Test]
    public void Transient_services_have_singleton_factories([Values(typeof(Func<Entity>), typeof(Func<OperationObject>))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
    }

    [Test]
    public void Types_without__with__methods_are_registered_as_singleton([Values(typeof(Singleton), typeof(Entities), typeof(Class))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
    }

    [Test]
    public void Singleton_types_with_interfaces_are_registered_as_implementations([Values(typeof(ITestObject), typeof(ISingleton))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBeSameAs(actual2);
        actual1.GetType().UnderlyingSystemType.ShouldBe(typeof(Singleton));
    }

    [Test]
    public void Transient_types_with_interfaces_are_registered_as_implementations()
    {
        var actual1 = GiveMe.The<ITransient>();
        var actual2 = GiveMe.The<ITransient>();

        actual1.ShouldNotBeSameAs(actual2);
        actual1.GetType().UnderlyingSystemType.ShouldBe(typeof(OperationObject));
    }

    [Test]
    public void Types_having_properties_with_public_getter_and_setter_are_registered_as_scoped()
    {
        var actual1 = GiveMe.The<Scoped>();
        var actual2 = GiveMe.The<Scoped>();
        var actual3 = GiveMe.The<IServiceProvider>().CreateScope().ServiceProvider.GetRequiredService<Scoped>();

        actual1.ShouldBeSameAs(actual2);
        actual1.ShouldNotBeSameAs(actual3);
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
        var action = () => GiveMe.The<Base>();

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(Base));
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
    public void Referenced_interfaces_are_not_registered([Values(typeof(IEquatable<Entity>), typeof(IScoped))] Type type )
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }
}
