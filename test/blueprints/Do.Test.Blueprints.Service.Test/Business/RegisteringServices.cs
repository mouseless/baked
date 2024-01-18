namespace Do.Test.Business;

public class RegisteringServices : TestServiceSpec
{
    [Test]
    public void Types_containing_a_method_named_with_and_returns_self_are_registered_as_transient([Values(typeof(Entity), typeof(OperationObject))] Type type)
    {
        var actual1 = GiveMe.TheService(type);
        var actual2 = GiveMe.TheService(type);

        actual1.ShouldNotBeNull();
        actual2.ShouldNotBeNull();
        actual1.ShouldNotBe(actual2);
    }

    [Test]
    public void Transient_services_have_singleton_factories([Values(typeof(Func<Entity>), typeof(Func<OperationObject>))] Type type)
    {
        var actual1 = GiveMe.TheService(type);
        var actual2 = GiveMe.TheService(type);

        actual1.ShouldNotBeNull();
        actual2.ShouldNotBeNull();
        actual1.ShouldBe(actual2);
    }

    [Test]
    public void Types_without__with__methods_are_registered_as_singleton([Values(typeof(Singleton), typeof(Entities), typeof(ClassService))] Type type)
    {
        var actual1 = GiveMe.TheService(type);
        var actual2 = GiveMe.TheService(type);

        actual1.ShouldNotBeNull();
        actual2.ShouldNotBeNull();
        actual1.ShouldBe(actual2);
    }

    [Test]
    public void Static_types_are_not_registered()
    {
        var actual = GiveMe.TheService(typeof(Static));

        actual.ShouldBeNull();
    }

    [Test]
    public void Value_types_are_not_registered([Values(typeof(Struct), typeof(Status))] Type type)
    {
        var actual = GiveMe.TheService(type);

        actual.ShouldBeNull();
    }

    [Test]
    public void Records_are_not_registered()
    {
        var actual = GiveMe.TheService<Record>();

        actual.ShouldBeNull();
    }

    [Test]
    public void Abstract_types_are_not_registered()
    {
        var actual = GiveMe.TheService<ServiceBase>();

        actual.ShouldBeNull();
    }

    [Test]
    public void Exception_types_are_not_registered([Values(typeof(SampleException), typeof(Exception))] Type type)
    {
        var actual = GiveMe.TheService(type);

        actual.ShouldBeNull();
    }

    [Test]
    public void System_types_are_not_registered([Values(typeof(int), typeof(string), typeof(Guid), typeof(List<>), typeof(Task<>))] Type type)
    {
        var actual = GiveMe.TheService(type);

        actual.ShouldBeNull();
    }

    [Test]
    public void Attributes_are_not_registered()
    {
        var actual = GiveMe.TheService<AuthorizationRequiredAttribute>();

        actual.ShouldBeNull();
    }
}
