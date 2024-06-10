using Baked.Test.ExceptionHandling;
using Baked.Test.Lifetime;
using Baked.Test.Orm;

namespace Baked.Test.Business;

public class RegisteringServices : TestServiceSpec
{
    [Test]
    public void Static_types_are_not_registered()
    {
        var action = () => GiveMe.The(typeof(Static));

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(Static));
    }

    [Test]
    public void Value_types_are_not_registered([Values(typeof(Struct), typeof(Enumeration))] Type type)
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
        var action = () => GiveMe.The<CustomAttribute>();

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(typeof(CustomAttribute));
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
        var nonPublicType = Activator.CreateInstance("Baked.Test.Recipe.Service", "Baked.Test.Business.Internal")?.GetType() ??
            throw new("`Baked.Test.Business.Internal` should have existed");
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
    public void User_defined_enumerable_types_are_not_registered([Values(typeof(CustomList), typeof(CustomDictionary))] Type type)
    {
        var action = () => GiveMe.The(type);

        action.ShouldThrowExceptionWithServiceNotRegisteredMessage(type);
    }
}