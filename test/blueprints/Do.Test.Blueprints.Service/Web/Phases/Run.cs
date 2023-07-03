using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web.Phases;

public class Run : IPhase
{
    public PhaseOrder Order => PhaseOrder.Latest;

    public bool CanInitialize(ApplicationContext context) => context.Has<WebApplication>();
    public void Initialize(ApplicationContext context)
    {
        var app = context.Get<WebApplication>();

        app.Run();
    }
}
