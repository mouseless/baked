namespace Do.Test.Architecture.Application;

public class RunningAnApplication : Spec
{
    [Test]
    public void Application_collects_phases_from_all_layers()
    {
        var phase1 = MockMe.APhase();
        var phase2 = MockMe.APhase();
        var phase3 = MockMe.APhase();
        var layer1 = MockMe.ALayer(phases: new[] { phase1, phase2 });
        var layer2 = MockMe.ALayer(phase: phase3);
        var app = GiveMe.AnApplication(layers: new[] { layer1, layer2 });

        app.Run();

        phase1.VerifyInitialized();
        phase2.VerifyInitialized();
        phase3.VerifyInitialized();
    }

    [Test]
    public void Application_initializes_each_phase_before_they_are_applied_to_layers()
    {
        var values = new List<string>();

        var phase = MockMe.APhase(onInitialize: () => values.Add("phase"));
        var layer = MockMe.ALayer(phase: phase, onApplyPhase: () => values.Add("layer"));
        var app = GiveMe.AnApplication(layer: layer);

        app.Run();

        Assert.That(values.First(), Is.EqualTo("phase"));
        Assert.That(values.Last(), Is.EqualTo("layer"));
    }

    [Test]
    public void Each_phase_is_applied_separately_to_all_layers()
    {
        var phase1 = MockMe.APhase();
        var phase2 = MockMe.APhase();
        var layer1 = MockMe.ALayer(phase: phase1);
        var layer2 = MockMe.ALayer(phase: phase2);

        var app = GiveMe.AnApplication(layers: new[] { layer1, layer2 });

        app.Run();

        layer1.VerifyApplied(phase1);
        layer1.VerifyApplied(phase2);
        layer2.VerifyApplied(phase1);
        layer2.VerifyApplied(phase2);
    }

    [Test]
    public void Application_provides_phases_with_a_context()
    {
        var phase = MockMe.APhase();
        var layer = MockMe.ALayer(phase: phase);

        var context = GiveMe.AnApplicationContext();
        var app = GiveMe.AnApplication(
            context: context,
            layer: layer
        );

        app.Run();

        phase.VerifyInitialized(context: context);
    }

    [Test]
    public void Application_context_allows_phases_to_add_objects()
    {
        var context = GiveMe.AnApplicationContext();

        context.Add(this);

        Assert.That(context.Has<RunningAnApplication>(), Is.True);
    }

    [Test]
    public void Application_context_can_check_or_get_objects_via_their_type()
    {
        var context = GiveMe.AnApplicationContext(content: this);

        Assert.That(context.Has<RunningAnApplication>(), Is.True);
        Assert.That(context.Get<RunningAnApplication>(), Is.EqualTo(this));
    }

    [Test]
    [Ignore("not implemented")]
    public void Application_resolves_which_phase_to_initialize_automatically_by_checking_if_context_is_ready_for_them() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void When_more_than_one_phase_is_ready_at_the_same_time__they_are_initialized_according_to_their_priorities() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Only_one_phase_can_have_earliest_and_latest_priorities_at_the_same_time() => Assert.Fail();
}
