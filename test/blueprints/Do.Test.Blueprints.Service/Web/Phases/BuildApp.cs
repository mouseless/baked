using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web.Phases;

public class BuildApp : IPhase
{
    public PhaseOrder Order => PhaseOrder.Latest;

    public bool Initialize(ApplicationContext context)
    {
        if (!context.Has<WebApplicationBuilder>()) { return false; }

        var build = context.Get<WebApplicationBuilder>();
        var app = build.Build();

        context.Add(app);

        return true;
    }
}
