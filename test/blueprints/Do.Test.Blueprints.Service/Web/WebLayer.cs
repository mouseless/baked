using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web;

public class WebLayer : LayerBase<WebLayer.Build>
{
    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new Build();
        yield return new Run();
    }

    protected override PhaseContext GetContext(Build phase) =>
        phase.CreateContextBuilder()
            .Add<IApplicationBuilder>(Context.Get<WebApplication>())
            .Add<IEndpointRouteBuilder>(Context.Get<WebApplication>())
            .Build();

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

    public class Build : PhaseBase<WebApplicationBuilder>
    {
        public Build() : base(PhaseOrder.Latest) { }

        protected override void Initialize(WebApplicationBuilder build)
        {
            var app = build.Build();

            Context.Add(app);
        }
    }

    public class Run : PhaseBase<WebApplication>
    {
        public Run() : base(PhaseOrder.Latest) { }

        protected override void Initialize(WebApplication app) => app.Run();
    }
}
