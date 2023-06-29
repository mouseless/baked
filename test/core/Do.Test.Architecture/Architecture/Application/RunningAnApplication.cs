namespace Do.Test.Architecture.Application;

public class RunningAnApplication : Spec
{
    [Test]
    public void Application_collects_phases_from_all_layers()
    {
        /*
        var build = GiveMe.ABuild();
        var phase = MockMe.APhase();
        var layer = MockMe.ALayer(thatConfigures: phase);

        var app = build.As(app =>
        {
            app.Layers.Add(layer);
        });

        app.Run();
        */

        Assert.Fail("not implemented");
    }

    [Test]
    [Ignore("not implemented")]
    public void Application_initializes_each_phase_before_they_are_applied_to_layers() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Each_phase_is_applied_separately_to_all_layers() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Application_provides_phases_with_a_context() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Application_context_allows_phases_to_add_objects() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Application_context_can_get__chech_or_remove_objects_via_their_type() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Application_resolves_which_phase_to_initialize_automatically_by_checking_if_context_is_ready_for_them() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void When_more_than_one_phase_is_ready_at_the_same_time__they_are_initialized_according_to_their_priorities() => Assert.Fail();
}
