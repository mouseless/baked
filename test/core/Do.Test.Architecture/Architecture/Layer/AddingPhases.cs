using Do.Architecture;

namespace Do.Test.Architecture.Layer;

public class AddingPhases : ArchitectureSpec
{
    class NoPhaseLayer : LayerBase { }

    [Test]
    public void Layer_returns_no_phases_by_default()
    {
        ILayer layer = new NoPhaseLayer();
        var phases = layer.GetPhases();

        phases.Count().ShouldBe(0);
    }

    class TwoPhaseLayer : LayerBase
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

    class IndependentAddsString : PhaseBase
    {
        readonly string _artifact;

        public IndependentAddsString(string artifact)
        {
            _artifact = artifact;
        }

        protected override void Initialize()
        {
            Context.Add(_artifact);
        }
    }

    [Test]
    public void Phases_have_initialization_step_before_getting_applied_so_that_they_prepare_and_add_objects_to_application_context()
    {
        var context = GiveMe.AnApplicationContext();
        IPhase phase = new IndependentAddsString(artifact: "test");
        GiveMe.AnApplication(context: context, phase: phase);

        phase.Initialize();

        context.ShouldHave("test");
    }

    class StringDependentAddsInt : PhaseBase<string>
    {
        readonly string _expectedString;
        readonly int _artifact;

        public StringDependentAddsInt(string expectedString, int artifact)
        {
            _expectedString = expectedString;
            _artifact = artifact;
        }

        protected override void Initialize(string dependency)
        {
            dependency.ShouldBe(_expectedString);

            Context.Add(_artifact);
        }
    }

    class StringAndIntDependentAddsBool : PhaseBase<string, int>
    {
        readonly string _expectedString;
        readonly int _expectedInt;
        readonly bool _artifact;

        public StringAndIntDependentAddsBool(string expectedString, int expectedInt, bool artifact)
        {
            _expectedString = expectedString;
            _expectedInt = expectedInt;
            _artifact = artifact;
        }

        protected override void Initialize(string dependency1, int dependency2)
        {
            dependency1.ShouldBe(_expectedString);
            dependency2.ShouldBe(_expectedInt);

            Context.Add(_artifact);
        }
    }

    class StringIntAndBoolDependentAddsChar : PhaseBase<string, int, bool>
    {
        readonly string _expectedString;
        readonly int _expectedInt;
        readonly bool _expectedBool;
        readonly char _artifact;

        public StringIntAndBoolDependentAddsChar(string expectedString, int expectedInt, bool expectedBool, char artifact)
        {
            _expectedString = expectedString;
            _expectedInt = expectedInt;
            _expectedBool = expectedBool;
            _artifact = artifact;
        }

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
        IPhase phase = new StringDependentAddsInt(expectedString: GiveMe.AString(), artifact: GiveMe.AnInt());
        var app = GiveMe.AnApplication(
            context: GiveMe.AnApplicationContext(content: GiveMe.AnInt()),
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
            context: context,
            phases:
            [
                new StringIntAndBoolDependentAddsChar(
                    expectedString: "test",
                    expectedInt: 42,
                    expectedBool: true,
                    artifact: 'a'
                ),
                new StringAndIntDependentAddsBool(
                    expectedString: "test",
                    expectedInt: 42,
                    artifact: true
                ),
                new StringDependentAddsInt(
                    expectedString: "test",
                    artifact: 42
                ),
                new IndependentAddsString(artifact: "test"),
            ]
        );

        app.Run();

        context.ShouldHave('a');
    }

    public class OrderedPhase(PhaseOrder _order)
        : PhaseBase(_order) { }

    [TestCase(PhaseOrder.Early)]
    [TestCase(PhaseOrder.Late)]
    public void Phases_can_run_earlier_or_later_than_normal(PhaseOrder order)
    {
        IPhase phase = new OrderedPhase(order);

        phase.Order.ShouldBe(order);
    }
}
