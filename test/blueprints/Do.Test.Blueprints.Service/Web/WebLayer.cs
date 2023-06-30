using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web;

public class WebLayer : ILayer
{
    public IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new Build();
        yield return new Route();
        yield return new Run();
    }

    public ConfigurationTarget GetConfigurationTarget(IPhase phase, ApplicationContext context) =>
        phase switch
        {
            Build => ConfigurationTarget.Create<IApplicationBuilder>(context.Get<WebApplication>()),
            Route => ConfigurationTarget.Create<IEndpointRouteBuilder>(context.Get<WebApplication>()),
            _ => ConfigurationTarget.Empty
        };

    public class CreateBuilder : IPhase
    {
        public PhaseOrder Order => PhaseOrder.Earliest;

        public bool CanInitialize(ApplicationContext context) =>
            true;

        public void Initialize(ApplicationContext context)
        {
            var build = WebApplication.CreateBuilder();

            context.Add(build);
            context.Add(build.Services);
        }
    }

    public class Build : IPhase
    {
        public PhaseOrder Order => PhaseOrder.Latest;

        public bool CanInitialize(ApplicationContext context) =>
            context.Has<WebApplicationBuilder>();

        public void Initialize(ApplicationContext context)
        {
            var build = context.Get<WebApplicationBuilder>();
            var app = build.Build();

            context.Add(app);
        }
    }

    public class Route : IPhase
    {
        public PhaseOrder Order => PhaseOrder.Normal;

        public bool CanInitialize(ApplicationContext context) =>
            context.Has<WebApplication>();

        public void Initialize(ApplicationContext context) { }
    }

    public class Run : IPhase
    {
        public PhaseOrder Order => PhaseOrder.Latest;

        public bool CanInitialize(ApplicationContext context) =>
            context.Has<WebApplication>();

        public void Initialize(ApplicationContext context)
        {
            var app = context.Get<WebApplication>();

            app.Run();
        }
    }
}
