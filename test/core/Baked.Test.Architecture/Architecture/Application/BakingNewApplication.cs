using System.Globalization;

namespace Baked.Test.Architecture.Application;

public class BakingNewApplication : ArchitectureSpec
{
    [Test]
    public void It_is_accessible_via_a_fluent_api()
    {
        Bake.New.ShouldBeAssignableTo<Bake>();
    }

    [Test]
    public void Application_is_baked_through_the_builder_object()
    {
        var bake = GiveMe.ABake();

        var actual = bake.Application(_ => { });

        actual.ShouldBeAssignableTo<Baked.Architecture.Application>();
    }

    [Test]
    public void Baking_is_done_using_the_invariant_culture()
    {
        var bake = GiveMe.ABake();

        bake.Application(_ => { });

        Thread.CurrentThread.CurrentCulture.ShouldBe(CultureInfo.InvariantCulture);
    }
}