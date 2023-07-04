using Do.Architecture;
using Do.Test.Blueprints.Service.Web.Phases;

namespace Do.Test.Blueprints.Service.Web;

public class WebLayer : LayerBase
{
    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new BuildApp();
        yield return new MapRoutes();
        yield return new Run();
    }

    protected override ConfigurationTarget GetConfigurationTarget(IPhase phase, ApplicationContext context) =>
        phase switch
        {
            BuildApp => ConfigurationTarget.Create<IApplicationBuilder>(context.Get<WebApplication>()),
            MapRoutes => ConfigurationTarget.Create<IEndpointRouteBuilder>(context.Get<WebApplication>()),
            _ => ConfigurationTarget.Empty
        };
}
