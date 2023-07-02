namespace Do.Test.Architecture.Application;

public class BuildingAnApplication : Spec
{
    [Test]
    public void Build_is_accessible_via_a_fluent_api()
    {
        Assert.That(Build.Application, Is.InstanceOf<Build>());
    }

    [Test]
    public void Application_is_built_through_the_builder()
    {
        var build = GiveMe.ABuild();

        var actual = build.As(_ => { });

        Assert.That(actual, Is.InstanceOf<Do.Architecture.Application>());
    }
}
