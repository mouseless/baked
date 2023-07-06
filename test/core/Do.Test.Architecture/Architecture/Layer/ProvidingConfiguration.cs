using Do.Architecture;

using static Do.Test.Architecture.Layer.ProvidingConfiguration.LayerX;
using static Do.Test.Architecture.Layer.ProvidingConfiguration.LayerY;
using static Do.Test.Architecture.Layer.ProvidingConfiguration.LayerZ;

namespace Do.Test.Architecture.Layer;

public class ProvidingConfiguration : Spec
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

        void TestCase<TLayer, TPhase, TConfiguration>()
            where TLayer : ILayer
            where TPhase : IPhase
        {
            var context = GiveMe.AnApplicationContext();
            var layer = GiveMe.A<TLayer>();
            var phase = GiveMe.A<TPhase>();

            var phaseContext = layer.GetContext(phase, context);

            var configured = false;
            phaseContext.ConfigurationTarget.Configure((TConfiguration configuration) => configured = true);

            Assert.That(configured, Is.True, $"{typeof(TLayer).Name} didn't provide {typeof(TConfiguration).Name} for {typeof(TPhase).Name}");
        }
    }

    [Test]
    public void Layer_returns_empty_context_for_an_unrelated_phase()
    {
        var context = GiveMe.AnApplicationContext();
        var layer = new LayerX() as ILayer;
        var phase = new DoB();

        var phaseContext = layer.GetContext(phase, context);

        Assert.That(phaseContext, Is.EqualTo(PhaseContext.Empty));
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

        using (layer.GetContext(phase, context))
        {
            Assert.That(context.Get<string>(), Is.EqualTo("before"));
        }

        Assert.That(context.Get<string>(), Is.EqualTo("after"));
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

        using (var phaseContext = twoTarget.GetContext(phase, context))
        {
            var configured = false;
            phaseContext.ConfigurationTarget.Configure((string first, string second) =>
            {
                Assert.That(first, Is.EqualTo("first"));
                Assert.That(second, Is.EqualTo("second"));

                configured = true;
            });

            Assert.That(configured, Is.True, "Two target phase context didn't get configured");
        }

        Assert.That(context.Get<string>(), Is.EqualTo("after two target"));
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

        using (var phaseContext = threeTarget.GetContext(phase, context))
        {
            var configured = false;
            phaseContext.ConfigurationTarget.Configure((string first, string second, string third) =>
            {
                Assert.That(first, Is.EqualTo("first"));
                Assert.That(second, Is.EqualTo("second"));
                Assert.That(third, Is.EqualTo("third"));

                configured = true;
            });

            Assert.That(configured, Is.True, "There target phase context didn't get configured");
        }

        Assert.That(context.Get<string>(), Is.EqualTo("after three target"));
    }

    [Test]
    [Ignore("not implemented")]
    public void Phase_context_accepts_multiple_targets_to_be_applied_separately_in_the_given_order() => Assert.Fail();
}

/*
public class LayerA : LayerBase<PhaseX, PhaseY, PhaseZ>
{
    public LayerAConfigurationX ConfigX { get; } = new();
    public LayerAConfigurationY1 ConfigY1 { get; } = new();
    public LayerAConfigurationY2 ConfigY2 { get; } = new();
    public LayerAConfigurationZA ConfigZA { get; } = new();
    public LayerAConfigurationZB ConfigZB { get; } = new();
    public LayerAConfigurationZC ConfigZC { get; } = new();

    protected override PhaseContext GetContext(PhaseX phase)
    {
        var services = Context.Get<IServiceCollection>();

        services.AddLayerAStuff();

        return phase.CreateContext(ConfigX,
            onDispose: () => services.ConfigureLayerA(ConfigX)
        );
    }

    protected override PhaseContext GetContext(PhaseY phase)
    {
        var app = Context.Get<IApplicationBuilder>();

        return phase.CreateContext(ConfigY1, ConfigY2,
            onDispose: () => app.UseLayerA(ConfigY1, ConfigY2)
        );
    }

    protected override PhaseContext GetContext(PhaseZ phase)
    {
        var app = Context.Get<IApplicationBuilder>();

        app.UseLayerA(ConfigZA);

        return phase.CreateMultipleContext(onDispose: () => app.UseLayerA(ConfigZB, ConfigZC))
            .Add(ConfigZA)
            .Add(ConfigZB, ConfigZC)
        ;
    }
 }
 */
