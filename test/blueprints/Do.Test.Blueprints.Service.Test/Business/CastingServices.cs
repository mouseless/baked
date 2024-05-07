using Do.Test.Business.Casting;

namespace Do.Test.Business;

public class CastingServices : TestServiceSpec
{
    [Test]
    public void Caster_services_are_registered_in_a_static_context_to_allow_casting_from_extension()
    {
        var classA = GiveMe.The<ClassA>();

        var cast = () => { ClassB _ = classA; };

        cast.ShouldNotThrow();
    }
}