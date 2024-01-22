namespace Do.Test.Business;
public class RegisteringServices : TestServiceSpec
{
    [Test]
    public void Types_containing_a_method_named_with_and_returns_self_are_registered_as_transient([Values(typeof(Entity), typeof(OperationObject))] Type type)
    {
        var actual1 = GiveMe.A(type);
        var actual2 = GiveMe.A(type);

        actual1.ShouldNotBe(actual2);
    }

    [Test]
    public void Transient_services_have_singleton_factories([Values(typeof(Func<Entity>), typeof(Func<OperationObject>))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBe(actual2);
    }

    [Test]
    public void Types_without__with__methods_are_registered_as_singleton([Values(typeof(Singleton), typeof(Entities), typeof(ClassService))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBe(actual2);
    }

    [Test]
    public void Singleton_types_with_interfaces_are_registered_as_implementations([Values(typeof(ITestObjectService), typeof(IService))] Type type)
    {
        var actual1 = GiveMe.The(type);
        var actual2 = GiveMe.The(type);

        actual1.ShouldBe(actual2);
        (actual1 as Singleton).ShouldNotBeNull();
    }

    [Test]
    public void Transient_types_with_interfaces_are_registered_as_implementations()
    {
        var actual1 = GiveMe.The<ITransientService>();
        var actual2 = GiveMe.The<ITransientService>();

        actual1.ShouldNotBe(actual2);
        (actual1 as OperationObject).ShouldNotBeNull();
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
        var action = () => GiveMe.The<ServiceBase>();

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(ServiceBase));
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
    public void Referenced_interfaces_are_not_registered()
    {
        var action = () => GiveMe.A(typeof(IEquatable<Entity>));

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(IEquatable<Entity>));
    }
}

public static class RegisteringServicesExtensions
{
    public static void ShouldThrowExceptionWithServiceNotRegisteredMessage(this Func<object> source, Type serviceType) =>
        source.ShouldThrow<Exception>().Message.ShouldBe($"No service for type '{serviceType}' has been registered.");
}
