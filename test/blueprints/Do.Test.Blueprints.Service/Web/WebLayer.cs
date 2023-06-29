using Do.Architecture;
using Do.DependencyInjection;

namespace Do.Test.Blueprints.Service.Web;

public class WebLayer : ILayer
{
    public IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new Build();
        yield return new Run();
    }

    public IApplicationBuilder Builder { get; } = default!;

    public ILayerTarget? GetConfigurationTarget(ApplicationContext context, IPhase phase)
    {
        if (phase == nameof(Run))
        {
            var target = context.Get<WebApplication>();

            return new LayerTarget<IApplicationBuilder>(target);
        }

        return null;
    }

    public class CreateBuilder : IPhase
    {
        public string Name => nameof(Build);
        public Priority Priority => Priority.High;

        public bool IsReady(ApplicationContext context) => true;

        public void Initialize(ApplicationContext context)
        {
            var build = WebApplication.CreateBuilder();

            context.Add(build);
            context.Add(build.Services);
        }
    }

    public class Build : IPhase
    {
        public string Name => nameof(Build);
        public Priority Priority => Priority.Low;

        public bool IsReady(ApplicationContext context) =>
            context.Has<WebApplicationBuilder>();

        public void Initialize(ApplicationContext context)
        {
            var build = context.Remove<WebApplicationBuilder>();
            var app = build.Build();

            context.Add(app);
        }
    }

    public class Run : IPhase
    {
        public string Name => nameof(Run);
        public Priority Priority => Priority.Low;

        public bool IsReady(ApplicationContext context) =>
            context.Has<WebApplication>();

        public void Initialize(ApplicationContext context)
        {
            var app = context.Remove<WebApplication>();

            app.Run();
        }
    }
}
