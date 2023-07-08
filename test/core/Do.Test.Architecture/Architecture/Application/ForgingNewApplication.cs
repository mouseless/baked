namespace Do.Test.Architecture.Application;

public class ForgingNewApplication : Spec
{
    [Test]
    public void Builder_is_accessible_via_a_fluent_api()
    {
        Assert.That(Forge.New, Is.InstanceOf<Forge>());
    }

    [Test]
    public void Application_is_built_through_the_builder()
    {
        var forgeNew = GiveMe.AForge();

        var actual = forgeNew.Application(_ => { });

        Assert.That(actual, Is.InstanceOf<Do.Architecture.Application>());
    }
}
