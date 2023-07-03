using Do.Architecture;

namespace Do.Test.Blueprints.Service.DependencyInjection.Phases;

public class AddServices : IPhase
{
    public PhaseOrder Order => PhaseOrder.Earliest;

    public bool CanInitialize(ApplicationContext context) => context.Has<IServiceCollection>();
    public void Initialize(ApplicationContext _) { }
}
