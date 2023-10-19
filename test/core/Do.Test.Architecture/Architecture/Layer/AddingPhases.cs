using Do.Architecture;

namespace Do.Test.Architecture.Layer;

public class AddingPhases : ArchitectureSpec
{
    public class NoPhaseLayer : LayerBase { }

    [Test]
    public void Layer_returns_no_phases_by_default()
    {
        ILayer layer = new NoPhaseLayer();
        var phases = layer.GetPhases();

        phases.Count().ShouldBe(0);
    }

    public class TwoPhaseLayer : LayerBase
    {
        protected override IEnumerable<IPhase> GetPhases()
        {
            yield return new DoA();
            yield return new DoB();
        }

        public class DoA : PhaseBase { }
        public class DoB : PhaseBase { }
    }

    [Test]
    public void Layer_overrides_base_method_to_add_new_phases()
    {
        ILayer layer = new TwoPhaseLayer();
        var phases = layer.GetPhases();

        phases.Count().ShouldBe(2);

        phases.ShouldContain(phase => phase is TwoPhaseLayer.DoA);
        phases.ShouldContain(phase => phase is TwoPhaseLayer.DoB);
    }

    public class InitializedPhase : PhaseBase
    {
        protected override void Initialize()
        {
            Context.Add("test");
        }
    }

    [Test]
    public void Phases_have_initialization_step_before_getting_applied_so_that_they_prepare_and_add_objects_to_application_context()
    {
        IPhase phase = new InitializedPhase();

        var context = GiveMe.AnApplicationContext();
        var application = GiveMe.AnApplicationWithPhase(context: context, phase: phase);

        application.Run();

        context.ShouldHave("test");
    }

    public class OneDependencyPhase : PhaseBase<string>
    {
        protected override void Initialize(string dependency) =>
            Context.Add(0);
    }

    public class TwoDependencyPhase : PhaseBase<string, int>
    {
        protected override void Initialize(string dependency1, int dependency2) =>
            Context.Add(true);
    }

    public class ThreeDependencyPhase : PhaseBase<string, int, bool>
    {
        protected override void Initialize(string dependency1, int dependency2, bool dependency3) =>
            Context.Add('a');
    }

    [Test]
    public void Gives_error_when_dependency_is_not_the_exact_type()
    {
        IPhase oneDependency = new OneDependencyPhase();

        GiveMe.AnApplicationWithPhase(context: GiveMe.AnApplicationContext(content: 5), phase: oneDependency);

        var initializeAction = () => oneDependency.Initialize();

        initializeAction.ShouldThrow<KeyNotFoundException>();
    }

    [Test]
    public void Phases_may_depend_on_one_or_more_objects_to_appear_in_context()
    {
        IPhase initializing = new InitializedPhase();
        IPhase oneDependency = new OneDependencyPhase();
        IPhase twoDependency = new TwoDependencyPhase();
        IPhase threeDependency = new ThreeDependencyPhase();

        var context = GiveMe.AnApplicationContext();

        GiveMe.AnApplicationWithPhases(context: context, phases: new[] { initializing, oneDependency, twoDependency, threeDependency });

        initializing.IsReady.ShouldBeTrue();
        oneDependency.IsReady.ShouldBeFalse();

        initializing.Initialize();

        oneDependency.IsReady.ShouldBeTrue();
        twoDependency.IsReady.ShouldBeFalse();

        oneDependency.Initialize();

        twoDependency.IsReady.ShouldBeTrue();
        threeDependency.IsReady.ShouldBeFalse();

        twoDependency.Initialize();

        threeDependency.IsReady.ShouldBeTrue();

        threeDependency.Initialize();

        context.ShouldHave('a');
    }

    public class OrderedPhase : PhaseBase
    {
        public OrderedPhase(PhaseOrder order) : base(order) { }
    }

    [TestCase(PhaseOrder.Early)]
    [TestCase(PhaseOrder.Late)]
    public void Phases_can_run_earlier_or_later_than_normal(PhaseOrder order)
    {
        IPhase phase = new OrderedPhase(order);

        phase.Order.ShouldBe(order);
    }
}
