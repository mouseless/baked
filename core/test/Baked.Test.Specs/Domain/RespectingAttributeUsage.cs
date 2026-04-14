using Baked.Domain.Model;
using Baked.Playground.Business;

namespace Baked.Test.Domain;

public class RespectingAttributeUsage : TestSpec
{
    public class TargetAllAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Class)]
    public class TargetClassAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Method)]
    public class TargetMethodAttribute : Attribute;

    [AttributeUsage(AttributeTargets.Property)]
    public class TargetPropertyAttribute : Attribute;

    [Test]
    public void Adds_attribute_to_class_when_target_matches()
    {
        var domain = GiveMe.TheDomainModel();
        var attributes = (IMutableAttributeCollection)domain.Types[typeof(Class)].GetMetadata().CustomAttributes;

        var action = () =>
        {
            attributes.Set(new TargetAllAttribute());
            attributes.Set(new TargetClassAttribute());
        };

        action.ShouldNotThrow();
    }

    [TestCase(typeof(TargetClassAttribute))]
    [TestCase(typeof(TargetMethodAttribute))]
    [TestCase(typeof(TargetPropertyAttribute))]
    public void Throws_invalid_operation_exception_when_target_does_not_match(Type attributeType)
    {
        var domain = GiveMe.TheDomainModel();
        var attributes = (IMutableAttributeCollection)domain.Types[typeof(Struct)].GetMetadata().CustomAttributes;

        var action = () =>
        {
            attributes.Set(((Attribute?)Activator.CreateInstance(attributeType)) ?? throw new());
        };

        action.ShouldThrow<InvalidOperationException>().Message.ShouldBe($"'{attributeType.Name}' does not have 'Struct' target");
    }
}