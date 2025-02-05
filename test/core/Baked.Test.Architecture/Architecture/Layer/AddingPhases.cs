using Baked.Architecture;

namespace Baked.Test.Architecture.Layer;

public class AddingPhases : ArchitectureSpec
{
    class NoPhaseLayer : LayerBase;

    [Test]
    public void Layer_returns_no_phases_by_default()
    {
        ILayer layer = new NoPhaseLayer();
        var phases = layer.GetStartPhases();

        phases.Count().ShouldBe(0);
    }

    class TwoPhaseLayer : LayerBase
    {
        protected override IEnumerable<IPhase> GetStartPhases()
        {
            yield return new DoA();
            yield return new DoB();
        }

        public class DoA : PhaseBase;
        public class DoB : PhaseBase;
    }

    [Test]
    public void Layer_overrides_base_method_to_add_new_phases()
    {
        ILayer layer = new TwoPhaseLayer();
        var phases = layer.GetStartPhases();

        phases.Count().ShouldBe(2);

        phases.ShouldContain(phase => phase is TwoPhaseLayer.DoA);
        phases.ShouldContain(phase => phase is TwoPhaseLayer.DoB);
    }

    class LayerWithGeneratePhases : LayerBase
    {
        protected override IEnumerable<IPhase> GetStartPhases()
        {
            yield return new RuntimePhase();
        }

        protected override IEnumerable<IPhase> GetGeneratePhases()
        {
            yield return new GeneratePhase();
        }

        public class GeneratePhase : PhaseBase;
        public class RuntimePhase : PhaseBase;
    }

    [Test]

    public void Layers_can_add_seperate_phases_for_generate_and_start()
    {
        ILayer layer = new LayerWithGeneratePhases();

        var phases = layer.GetStartPhases();

        phases.Count().ShouldBe(1);
        phases.ShouldContain(phase => phase is LayerWithGeneratePhases.RuntimePhase);

        var generatePhases = layer.GetGeneratePhases();

        generatePhases.Count().ShouldBe(1);
        generatePhases.ShouldContain(phase => phase is LayerWithGeneratePhases.GeneratePhase);
    }

    class IndependentAddsString(string _artifact)
        : PhaseBase
    {
        protected override void Initialize()
        {
            Context.Add(_artifact);
        }
    }

    [Test]
    public void Phases_have_initialization_step_before_getting_applied_so_that_they_prepare_and_add_objects_to_application_context()
    {
        var context = GiveMe.AnApplicationContext();
        IPhase phase = new IndependentAddsString(_artifact: "test");
        GiveMe.AnApplication(startContext: context, phase: phase);

        phase.Initialize();

        context.ShouldHave("test");
    }

    public class Phase : PhaseBase;

    [Test]
    public void Base_initialization_does_not_add_any_objects_to_application_context()
    {
        var applicationContext = GiveMe.AnApplicationContext();
        var emptyContext = GiveMe.AnApplicationContext();

        IPhase phase = new Phase();

        phase.Initialize();

        applicationContext.ShouldBeEquivalentTo(emptyContext);
    }

    class StringDependentAddsInt(string _expectedString, int _artifact)
        : PhaseBase<string>
    {
        protected override void Initialize(string dependency)
        {
            dependency.ShouldBe(_expectedString);

            Context.Add(_artifact);
        }
    }

    class StringAndIntDependentAddsBool(string _expectedString, int _expectedInt, bool _artifact)
        : PhaseBase<string, int>
    {
        protected override void Initialize(string dependency1, int dependency2)
        {
            dependency1.ShouldBe(_expectedString);
            dependency2.ShouldBe(_expectedInt);

            Context.Add(_artifact);
        }
    }

    class StringIntAndBoolDependentAddsChar(string _expectedString, int _expectedInt, bool _expectedBool, char _artifact)
        : PhaseBase<string, int, bool>
    {
        protected override void Initialize(string dependency1, int dependency2, bool dependency3)
        {
            dependency1.ShouldBe(_expectedString);
            dependency2.ShouldBe(_expectedInt);
            dependency3.ShouldBe(_expectedBool);

            Context.Add(_artifact);
        }
    }

    [Test]
    public void Gives_error_when_dependency_is_not_the_exact_type()
    {
        IPhase phase = new StringDependentAddsInt(_expectedString: GiveMe.AString(), _artifact: GiveMe.AnInt());
        var app = GiveMe.AnApplication(
            startContext: GiveMe.AnApplicationContext(content: GiveMe.AnInt()),
            phase: phase
        );

        var initializeAction = () => phase.Initialize();

        initializeAction.ShouldThrow<KeyNotFoundException>();
    }

    [Test]
    public void Phases_may_depend_on_one_or_more_objects_to_appear_in_context()
    {
        var context = GiveMe.AnApplicationContext();
        var app = GiveMe.AnApplication(
            startContext: context,
            phases:
            [
                new StringIntAndBoolDependentAddsChar(
                    _expectedString: "test",
                    _expectedInt: 42,
                    _expectedBool: true,
                    _artifact: 'a'
                ),
                new StringAndIntDependentAddsBool(
                    _expectedString: "test",
                    _expectedInt: 42,
                    _artifact: true
                ),
                new StringDependentAddsInt(
                    _expectedString: "test",
                    _artifact: 42
                ),
                new IndependentAddsString(_artifact: "test"),
            ]
        );

        app.Run();

        context.ShouldHave('a');
    }

    public class OrderedPhase(PhaseOrder _order)
        : PhaseBase(_order);

    [TestCase(PhaseOrder.Early)]
    [TestCase(PhaseOrder.Late)]
    public void Phases_can_run_earlier_or_later_than_normal(PhaseOrder order)
    {
        IPhase phase = new OrderedPhase(order);

        phase.Order.ShouldBe(order);
    }
}