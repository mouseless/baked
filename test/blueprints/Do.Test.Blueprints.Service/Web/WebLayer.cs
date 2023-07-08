using Do.Architecture;

using static Do.Test.Blueprints.Service.Web.WebLayer;

namespace Do.Test.Blueprints.Service.Web;

public class WebLayer : LayerBase<Build>
{
    protected override PhaseContext GetContext(Build phase) =>
        phase.CreateContextBuilder()
            .Add<IApplicationBuilder>(Context.Get<WebApplication>())
            .Add<IEndpointRouteBuilder>(Context.Get<WebApplication>())
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new Build();
        yield return new Run();
    }

    public class CreateBuilder : PhaseBase
    {
        public CreateBuilder() : base(PhaseOrder.Earliest) { }

        protected override void Initialize()
        {
            var build = WebApplication.CreateBuilder();

            Context.Add(build);
        }
    }

    public class Build : PhaseBase<WebApplicationBuilder, IServiceCollection>
    {
        public Build() : base(PhaseOrder.Latest) { }

        protected override void Initialize(WebApplicationBuilder build, IServiceCollection services)
        {
            foreach (var service in services)
            {
                build.Services.Add(service);
            }

            var app = build.Build();

            Context.Add(app);
        }
    }

    class Run : PhaseBase<WebApplication>
    {
        public Run() : base(PhaseOrder.Latest) { }

        protected override void Initialize(WebApplication app) => app.Run();
    }
}
