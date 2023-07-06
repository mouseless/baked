using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web;

public class WebLayer : LayerBase<WebLayer.BuildApp, WebLayer.MapRoutes>
{
    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new BuildApp();
        yield return new MapRoutes();
        yield return new Run();
    }

    protected override PhaseContext GetContext(BuildApp phase) =>
        phase.CreateContext<IApplicationBuilder>(Context.Get<WebApplication>());

    protected override PhaseContext GetContext(MapRoutes phase) =>
        phase.CreateContext<IEndpointRouteBuilder>(Context.Get<WebApplication>());

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
