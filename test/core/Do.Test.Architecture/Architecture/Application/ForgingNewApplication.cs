namespace Do.Test.Architecture.Application;

public class ForgingNewApplication : ArchitectureSpec
{
    [Test]
    public void It_is_accessible_via_a_fluent_api()
    {
        Forge.New.ShouldBeAssignableTo<Forge>();
    }

    [Test]
    public void Application_is_forged_through_the_builder_object()
    {
        var forge = GiveMe.AForge();

        var actual = forge.Application(_ => { });

        actual.ShouldBeAssignableTo<Do.Architecture.Application>();
    }
}
