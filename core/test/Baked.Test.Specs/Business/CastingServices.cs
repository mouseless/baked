using Baked.Playground.Business.Casting;

namespace Baked.Test.Business;

public class CastingServices : TestSpec
{
    [Test]
    public void Caster_services_are_registered_in_a_static_context_to_allow_casting_from_extension()
    {
        var classA = GiveMe.The<ClassA>();
        var classB = GiveMe.The<ClassB>();
        GiveMe.The<ClassB>();

        ClassB actual = classA;

        actual.ShouldBeSameAs(classB);
    }
}