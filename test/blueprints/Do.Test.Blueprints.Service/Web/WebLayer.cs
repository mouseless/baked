using Do.Architecture;

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

    public class CreateBuilder : PhaseBase
    {
        public CreateBuilder() : base(PhaseOrder.Earliest) { }

        protected override void Initialize()
        {
            var build = WebApplication.CreateBuilder();

            Context.Add(build);
            Context.Add(build.Services);
        }
    }

    public class BuildApp : PhaseBase<WebApplicationBuilder>
    {
        public BuildApp() : base(PhaseOrder.Latest) { }

        protected override void Initialize(WebApplicationBuilder build)
        {
            var app = build.Build();

            Context.Add(app);
        }
    }

    public class MapRoutes : PhaseBase<WebApplication>
    {
        protected override void Initialize(WebApplication _) { }
    }

    public class Run : PhaseBase<WebApplication>
    {
        public Run() : base(PhaseOrder.Latest) { }

        protected override void Initialize(WebApplication app) => app.Run();
    }
}
