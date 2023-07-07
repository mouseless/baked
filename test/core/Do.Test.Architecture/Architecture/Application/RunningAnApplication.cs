using Do.Architecture;

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
    [Ignore("not implemented")]
    public void Phase_contexts_should_be_disposed_after_phase_is_applied() => Assert.Fail();

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

        context.ShouldHave(this);
    }

    [Test]
    public void Application_context_can_check_if_it_has_an_object_via_its_type()
    {
        var context = GiveMe.AnApplicationContext(content: this);

        var hasContent = context.Has<RunningAnApplication>();

        Assert.That(hasContent, Is.True);
    }

    [Test]
    public void Application_context_can_get_an_object_via_its_type()
    {
        var context = GiveMe.AnApplicationContext(content: this);

        var content = context.Get<RunningAnApplication>();

        Assert.That(content, Is.EqualTo(this));
    }

    [Test]
    public void Application_resolves_which_phase_to_initialize_automatically_by_checking_if_they_are_ready()
    {
        var phases = new List<string>();

        var phaseA = MockMe.APhase(onInitialize: () => phases.Add("phase a"), isReady: () => phases.Contains("phase b"));
        var phaseB = MockMe.APhase(onInitialize: () => phases.Add("phase b"), isReady: () => phases.Contains("phase c"));
        var phaseC = MockMe.APhase(onInitialize: () => phases.Add("phase c"));
        var layer = MockMe.ALayer(phases: new[] { phaseA, phaseB, phaseC });

        var app = GiveMe.AnApplication(layer: layer);

        app.Run();

        Assert.That(phases[0], Is.EqualTo("phase c"));
        Assert.That(phases[1], Is.EqualTo("phase b"));
        Assert.That(phases[2], Is.EqualTo("phase a"));
    }

    [Test]
    public void When_more_than_one_phase_is_ready_at_the_same_time__they_are_initialized_according_to_their_priorities()
    {
        var phases = new List<string>();

        var phaseA = MockMe.APhase(onInitialize: () => phases.Add("phase a"), order: PhaseOrder.Late);
        var phaseB = MockMe.APhase(onInitialize: () => phases.Add("phase b"), order: PhaseOrder.Early);
        var layer = MockMe.ALayer(phases: new[] { phaseA, phaseB });

        var app = GiveMe.AnApplication(layer: layer);

        app.Run();

        Assert.That(phases[0], Is.EqualTo("phase b"));
        Assert.That(phases[1], Is.EqualTo("phase a"));
    }

    [Test]
    public void There_are_five_phase_order_values()
    {
        Assert.That((int)PhaseOrder.Earliest, Is.EqualTo(1));
        Assert.That((int)PhaseOrder.Early, Is.EqualTo(2));
        Assert.That((int)PhaseOrder.Normal, Is.EqualTo(3));
        Assert.That((int)PhaseOrder.Late, Is.EqualTo(4));
        Assert.That((int)PhaseOrder.Latest, Is.EqualTo(5));
    }

    [TestCase(PhaseOrder.Earliest)]
    [TestCase(PhaseOrder.Latest)]
    public void Only_one_phase_can_have_earliest_and_latest_priorities_at_the_same_time(PhaseOrder order)
    {
        var phaseA = MockMe.APhase(order: order);
        var phaseB = MockMe.APhase(order: order);
        var layer = MockMe.ALayer(phases: new[] { phaseA, phaseB });

        var app = GiveMe.AnApplication(layer: layer);

        Assert.That(() => app.Run(), Throws.TypeOf<OverlappingPhaseException>());
    }

    [Test]
    public void When_a_phase_never_gets_ready_it_gives_error()
    {
        var phase = MockMe.APhase(isReady: () => false);
        var layer = MockMe.ALayer(phase: phase);

        var app = GiveMe.AnApplication(layer: layer);

        Assert.That(() => app.Run(), Throws.TypeOf<CannotProceedException>());
    }
}
