using Do.Architecture;

namespace Do.Test.Blueprints.Service.Web.Phases;

public class MapRoutes : IPhase
{
    public PhaseOrder Order => PhaseOrder.Normal;

    public bool CanInitialize(ApplicationContext context) => context.Has<WebApplication>();
    public void Initialize(ApplicationContext _) { }
}
