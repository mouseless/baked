using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web.Phases;

public class Run : IPhase
{
    public PhaseOrder Order => PhaseOrder.Latest;

    public bool Initialize(ApplicationContext context)
    {
        if (!context.Has<WebApplication>()) { return false; }

        var app = context.Get<WebApplication>();

        app.Run();

        return true;
    }
}
