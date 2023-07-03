namespace Do.Test.Architecture.Layer;

public class AddingPhases
{
    [Test]
    [Ignore("not implemented")]
    public void Layers_are_asked_to_add_new_phases() => Assert.Fail();

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
public class OnePhaseLayer : LayerBase<AddServices>
{
}

public class TwoPhaseLayer : LayerBase<PhaseA, PhaseB>
{
}

support up to 3 (?) phases


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
