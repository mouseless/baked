using Do.Architecture;

using static Do.Test.Architecture.Layer.ProvidingConfiguration.LayerX;
using static Do.Test.Architecture.Layer.ProvidingConfiguration.LayerY;
using static Do.Test.Architecture.Layer.ProvidingConfiguration.LayerZ;

namespace Do.Test.Architecture.Layer;

public class ProvidingConfiguration : ArchitectureSpec
{
    public record LayerXConfigurationA();

    public class LayerX : LayerBase<DoA>
    {
        protected override PhaseContext GetContext(DoA phase) => phase.CreateContext(new LayerXConfigurationA());

        public class DoA : PhaseBase { }
    }

    public record LayerYConfigurationA();
    public record LayerYConfigurationB();

    public class LayerY : LayerBase<DoA, DoB>
    {
        protected override PhaseContext GetContext(DoA phase) => phase.CreateContext(new LayerYConfigurationA());
        protected override PhaseContext GetContext(DoB phase) => phase.CreateContext(new LayerYConfigurationB());

        public class DoB : PhaseBase { }
    }

    public record LayerZConfigurationA();
    public record LayerZConfigurationB();
    public record LayerZConfigurationC();

    public class LayerZ : LayerBase<DoA, DoB, DoC>
    {
        protected override PhaseContext GetContext(DoA phase) => phase.CreateContext(new LayerZConfigurationA());
        protected override PhaseContext GetContext(DoB phase) => phase.CreateContext(new LayerZConfigurationB());
        protected override PhaseContext GetContext(DoC phase) => phase.CreateContext(new LayerZConfigurationC());

        public class DoC : PhaseBase { }
    }

    [Test]
    public void A_layer_provides_configuration_for_up_to_three_phases()
    {
        TestCase<LayerX, DoA, LayerXConfigurationA>();

        TestCase<LayerY, DoA, LayerYConfigurationA>();
        TestCase<LayerY, DoB, LayerYConfigurationB>();

        TestCase<LayerZ, DoA, LayerZConfigurationA>();
        TestCase<LayerZ, DoB, LayerZConfigurationB>();
        TestCase<LayerZ, DoC, LayerZConfigurationC>();

        void TestCase<TLayer, TPhase, TTarget>()
            where TLayer : ILayer
            where TPhase : IPhase
        {
            var context = GiveMe.AnApplicationContext();
            var layer = GiveMe.AnInstanceOf<TLayer>();
            var phase = GiveMe.AnInstanceOf<TPhase>();
            var target = GiveMe.AnInstanceOf<TTarget>();

            var phaseContext = layer.GetContext(phase, context);

            phaseContext.ShouldConfigureTarget(target);
        }
    }

    [Test]
    public void Layer_returns_empty_context_for_an_unrelated_phase()
    {
        var context = GiveMe.AnApplicationContext();
        var layer = new LayerX() as ILayer;
        var phase = new DoB();

        var phaseContext = layer.GetContext(phase, context);

        phaseContext.ShouldBe(PhaseContext.Empty);
    }

    public class BeforeAfterLayer : LayerBase<DoA>
    {
        protected override PhaseContext GetContext(DoA phase)
        {
            Context.Add("before");

            return phase.CreateContext(new object(),
                onDispose: () => Context.Add("after")
            );
        }
    }

    [Test]
    public void Layer_can_do_stuff_before_and_after_phase_is_applied()
    {
        var context = GiveMe.AnApplicationContext();
        var layer = new BeforeAfterLayer() as ILayer;
        var phase = new DoA();

        var phaseContext = layer.GetContext(phase, context);

        context.ShouldHave("before");
        phaseContext.ShouldAddValueToContextOnDispose("after", context);
    }

    public class TwoTargetLayer : LayerBase<DoA>
    {
        protected override PhaseContext GetContext(DoA phase) =>
            phase.CreateContext("first", "second",
                onDispose: () => Context.Add("after two target")
            );
    }

    [Test]
    public void Phase_context_allows_two_targets_for_a_single_iteration()
    {
        var context = GiveMe.AnApplicationContext();
        var phase = new DoA();
        var twoTarget = new TwoTargetLayer() as ILayer;

        var phaseContext = twoTarget.GetContext(phase, context);

        phaseContext.ShouldConfigureTwoTargets("first", "second");
        phaseContext.ShouldAddValueToContextOnDispose("after two target", context);
    }

    public class ThreeTargetLayer : LayerBase<DoA>
    {
        protected override PhaseContext GetContext(DoA phase) =>
            phase.CreateContext("first", "second", "third",
                onDispose: () => Context.Add("after three target")
            );
    }

    [Test]
    public void Phase_context_allows_three_targets_for_a_single_iteration()
    {
        var context = GiveMe.AnApplicationContext();
        var phase = new DoA();
        var threeTarget = new ThreeTargetLayer() as ILayer;

        var phaseContext = threeTarget.GetContext(phase, context);

        phaseContext.ShouldConfigureThreeTargets("first", "second", "third");
        phaseContext.ShouldAddValueToContextOnDispose("after three target", context);
    }

    public class MultiTargetLayer : LayerBase<DoA>
    {
        protected override PhaseContext GetContext(DoA phase) =>
            phase.CreateContextBuilder()
                .Add("first")
                .Add("first", "second")
                .Add("first", "second", "third")
                .OnDispose(() => Context.Add("after multi target"))
                .Build();
    }

    [Test]
    public void Phase_context_accepts_multiple_targets_to_be_applied_separately_in_the_given_order()
    {
        var context = GiveMe.AnApplicationContext();
        var phase = new DoA();
        var multiTarget = new MultiTargetLayer() as ILayer;

        var phaseContext = multiTarget.GetContext(phase, context);

        phaseContext.ShouldConfigureTarget("first");
        phaseContext.ShouldConfigureTwoTargets("first", "second");
        phaseContext.ShouldConfigureThreeTargets("first", "second", "third");
        phaseContext.ShouldAddValueToContextOnDispose("after multi target", context);
    }
}
