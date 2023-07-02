using Do.Architecture;

namespace Do.Test.Blueprints.Service.DependencyInjection.Phases;

public class AddServices : IPhase
{
    public PhaseOrder Order => PhaseOrder.Earliest;

    public bool Initialize(ApplicationContext context) => context.Has<IServiceCollection>();
}
