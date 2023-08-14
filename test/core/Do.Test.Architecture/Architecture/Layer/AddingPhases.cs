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
        var context = GiveMe.AnApplicationContext();

        IPhase phase = new InitializedPhase();

        phase.Initialize(context);

        context.ShouldHave("test");
    }

    [Ignore("Not implemented")]
    [Test]
    public void App_context_throws_a_not_found_exception_if_given_type_does_not_exist_in_context()
    {
        var context = GiveMe.AnApplicationContext("Test");

        Should.Throw<NotFoundException>(() =>
        {
            context.Get<bool>();
        });
    }

    [Ignore("Not implemented")]
    [Test]
    public void App_context_not_found_exception_message_states_context_is_empty()
    {
        var context = GiveMe.AnApplicationContext();

        Should.Throw<NotFoundException>(() =>
        {
            context.Get<string>();
        }).Message.ShouldBeSameAs("Context is empty");
    }

    [Ignore("Not implemented")]
    [Test]
    public void App_context_not_found_exception_message_includes_any_type_implementing_or_extending_given_type()
    {
        var context = GiveMe.AnApplicationContext("Test");

        Should.Throw<NotFoundException>(() =>
        {
            context.Get<object>();
        }).Message.ShouldBeSameAs("Given type could not be found in ApplicationContext. Did you mean ? String");
    }

    [Ignore("Not implemented")]
    [Test]
    public void App_context_not_found_exception_message_includes_all_types_if_no_related_type_is_found()
    {
        var context = GiveMe.AnApplicationContext();
        context.Add("Test");
        context.Add(5);

        Should.Throw<NotFoundException>(() =>
        {
            context.Get<bool>();
        }).Message.ShouldBeSameAs("Given type could not be found in ApplicationContext. Did you mean ? String, Int32");
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

        initializing.IsReady(context).ShouldBeTrue();
        oneDependency.IsReady(context).ShouldBeFalse();

        initializing.Initialize(context);

        oneDependency.IsReady(context).ShouldBeTrue();
        twoDependency.IsReady(context).ShouldBeFalse();

        oneDependency.Initialize(context);

        twoDependency.IsReady(context).ShouldBeTrue();
        threeDependency.IsReady(context).ShouldBeFalse();

        twoDependency.Initialize(context);

        threeDependency.IsReady(context).ShouldBeTrue();

        threeDependency.Initialize(context);

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
