using Do.Architecture;

namespace Do.Test.Architecture.Layer;

public class AddingPhases : Spec
{
    public class NoPhaseLayer : LayerBase { }

    [Test]
    public void Layer_returns_no_phases_by_default()
    {
        ILayer layer = new NoPhaseLayer();
        var phases = layer.GetPhases();

        Assert.That(phases, Has.Exactly(0).Items);
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

        Assert.That(phases, Has.Exactly(2).Items);
        Assert.That(phases, Has.One.TypeOf<TwoPhaseLayer.DoA>());
        Assert.That(phases, Has.One.TypeOf<TwoPhaseLayer.DoB>());
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
        var context = GiveMe.AnApplicationContext();

        IPhase phase = new InitializedPhase();

        phase.Initialize(context);

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
    public void Phases_may_depend_on_one_or_more_objects_to_appear_in_context()
    {
        var context = GiveMe.AnApplicationContext();

        IPhase initializing = new InitializedPhase();
        IPhase oneDependency = new OneDependencyPhase();
        IPhase twoDependency = new TwoDependencyPhase();
        IPhase threeDependency = new ThreeDependencyPhase();

        Assert.That(initializing.IsReady(context), Is.True);
        Assert.That(oneDependency.IsReady(context), Is.False);

        initializing.Initialize(context);

        Assert.That(oneDependency.IsReady(context), Is.True);
        Assert.That(twoDependency.IsReady(context), Is.False);

        oneDependency.Initialize(context);

        Assert.That(twoDependency.IsReady(context), Is.True);
        Assert.That(threeDependency.IsReady(context), Is.False);

        twoDependency.Initialize(context);

        Assert.That(threeDependency.IsReady(context), Is.True);

        threeDependency.Initialize(context);

        Assert.That(context.Get<char>(), Is.EqualTo('a'));
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

        Assert.That(phase.Order, Is.EqualTo(order));
    }
}
