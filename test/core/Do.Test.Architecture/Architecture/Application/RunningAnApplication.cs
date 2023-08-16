using Do.Architecture;

namespace Do.Test.Architecture.Application;

public class RunningAnApplication : ArchitectureSpec
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

        values.First().ShouldBe("phase");
        values.Last().ShouldBe("layer");
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
    public void Phase_contexts_should_be_disposed_after_phase_is_applied()
    {
        var disposed = false;
        var phaseContext = GiveMe.APhaseContext(onDispose: () => disposed = true);
        var layer = MockMe.ALayer(phaseContext: phaseContext);

        var app = GiveMe.AnApplication(layer: layer);

        app.Run();

        disposed.ShouldBeTrue("Phase context didn't get disposed");
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

        context.ShouldHave(this);
    }

    [Test]
    public void Application_context_can_check_if_it_has_an_object_via_its_type()
    {
        var context = GiveMe.AnApplicationContext(content: this);

        var hasContent = context.Has<RunningAnApplication>();

        hasContent.ShouldBeTrue();
    }

    [Test]
    public void Application_context_can_get_an_object_via_its_type()
    {
        var context = GiveMe.AnApplicationContext(content: this);

        var content = context.Get<RunningAnApplication>();

        content.ShouldBe(this);
    }

    [Test]
    public void Application_context_throws_a_not_found_exception_if_given_type_does_not_exist_in_context()
    {
        var context = GiveMe.AnApplicationContext(content: 5);

        var getAction = () => context.Get<string>();

        getAction.ShouldThrow<KeyNotFoundException>();
    }

    [Test]
    public void App_context_not_found_exception_message_states_context_is_empty()
    {
        var context = GiveMe.AnApplicationContext();

        var getAction = () => context.Get<string>();

        getAction.ShouldThrow<KeyNotFoundException>().Message.ShouldBe(
            "'String' does not exist in context because it is empty."
        );
    }

    [Test]
    public void Application_context_not_found_exception_message_includes_any_type_implementing_or_extending_given_type()
    {
        var context = GiveMe.AnApplicationContext(content1: "Test", content2: 5);

        var getAction = () => context.Get<object>();

        getAction.ShouldThrow<KeyNotFoundException>().Message.ShouldBe(
            "'Object' does not exist in context. Did you mean: 'String', 'Int32'?"
        );
    }

    [Test]
    public void Application_context_not_found_exception_message_includes_all_types_if_no_related_type_is_found()
    {
        var context = GiveMe.AnApplicationContext(content1: 'c', content2: 5 );

        var getAction = () => context.Get<string>();

        getAction.ShouldThrow<KeyNotFoundException>().Message.ShouldBe(
           "'String' does not exist in context. Available types are: 'Char', 'Int32'"
       );
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

        phases[0].ShouldBe("phase c");
        phases[1].ShouldBe("phase b");
        phases[2].ShouldBe("phase a");
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

        phases[0].ShouldBe("phase b");
        phases[1].ShouldBe("phase a");
    }

    [Test]
    public void There_are_five_phase_order_values()
    {
        ((int)PhaseOrder.Earliest).ShouldBe(1);
        ((int)PhaseOrder.Early).ShouldBe(2);
        ((int)PhaseOrder.Normal).ShouldBe(3);
        ((int)PhaseOrder.Late).ShouldBe(4);
        ((int)PhaseOrder.Latest).ShouldBe(5);
    }

    [TestCase(PhaseOrder.Earliest)]
    [TestCase(PhaseOrder.Latest)]
    public void Only_one_phase_can_have_earliest_and_latest_priorities_at_the_same_time(PhaseOrder order)
    {
        var phaseA = MockMe.APhase(order: order);
        var phaseB = MockMe.APhase(order: order);
        var layer = MockMe.ALayer(phases: new[] { phaseA, phaseB });

        var app = GiveMe.AnApplication(layer: layer);
        var action = () => app.Run();

        action.ShouldThrow<OverlappingPhaseException>();
    }

    [Test]
    public void When_a_phase_never_gets_ready_it_gives_error()
    {
        var phase = MockMe.APhase(isReady: () => false);
        var layer = MockMe.ALayer(phase: phase);

        var app = GiveMe.AnApplication(layer: layer);
        var action = () => app.Run();

        action.ShouldThrow<CannotProceedException>();
    }
}
