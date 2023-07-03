using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web.Phases;

public class BuildApp : IPhase
{
    public PhaseOrder Order => PhaseOrder.Latest;

    public bool CanInitialize(ApplicationContext context) => context.Has<WebApplicationBuilder>();
    public void Initialize(ApplicationContext context)
    {
        var build = context.Get<WebApplicationBuilder>();
        var app = build.Build();

        context.Add(app);
    }
}
