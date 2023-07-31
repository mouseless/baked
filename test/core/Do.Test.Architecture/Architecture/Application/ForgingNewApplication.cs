namespace Do.Test.Architecture.Application;

public class ForgingNewApplication : Spec
{
    [Test]
    public void It_is_accessible_via_a_fluent_api()
    {
        Forge.New.ShouldBeAssignableTo(typeof(Forge));
    }

    [Test]
    public void Application_is_forged_through_the_builder_object()
    {
        var forge = GiveMe.AForge();

        var actual = forge.Application(_ => { });

        actual.ShouldBeAssignableTo(typeof(Do.Architecture.Application));
    }
}
