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
            yield return new Phase();
            yield return new Phase();
        }
    }

    public class Phase : PhaseBase { }

    [Test]
    public void Layer_overrides_base_method_to_add_new_phases()
    {
        ILayer layer = new TwoPhaseLayer();
        var phases = layer.GetPhases();

        Assert.That(phases, Has.Exactly(2).Items);
        Assert.That(phases, Has.All.TypeOf<Phase>());
    }

    [Test]
    [Ignore("not implemented")]
    public void Phases_have_initialization_before_getting_applied() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Phases_may_depend_on_one_or_more_objects_to_appear_in_context() => Assert.Fail();

    [Test]
    [Ignore("not implemented")]
    public void Phases_can_run_earlier_or_later_than_normal() => Assert.Fail();
}

/*
public class NoDependency : PhaseBase
{
    public override PhaseOrder Order => PhaseOrder.Early;

    public override Initialize(IServiceCollection serviceCollection)
    {

    }
}

public class OneDependency : PhaseBase<IServiceCollection>
{
    public override PhaseOrder Order => PhaseOrder.Early;

    public override Initialize(IServiceCollection serviceCollection)
    {

    }
}

public class TwoDependencies : PhaseBase<WebApplicationBuilder, IServiceCollection>
{
    public override Initialize(WebApplicationBuilder build, IServiceCollection serviceCollection)
    {

    }
}

support up to 3 dependencies
*/
