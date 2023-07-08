namespace Do.Test.Architecture.Application;

public class ForgingNewApplication : Spec
{
    [Test]
    public void It_is_accessible_via_a_fluent_api()
    {
        Assert.That(Forge.New, Is.InstanceOf<Forge>());
    }

    [Test]
    public void Application_is_forged_through_the_builder_object()
    {
        var forge = GiveMe.AForge();

        var actual = forge.Application(_ => { });

        Assert.That(actual, Is.InstanceOf<Do.Architecture.Application>());
    }
}
