using Do.Architecture;

namespace Do.Test.Blueprints.Service.DependencyInjection;

public class DependencyInjectionLayer : ILayer
{
    public IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices(this);
    }

    public IServiceCollection ServiceCollection { get; internal set; } = default!;

    public ConfigurationTarget GetConfigurationTarget(ApplicationContext context) =>
        context.Phase switch
        {
            AddServices => ConfigurationTarget.Create(context.Get<IServiceCollection>()),
            _ => ConfigurationTarget.Empty
        };

}

public class AddServices : IPhase
{
    private readonly DependencyInjectionLayer _layer;
    public AddServices(DependencyInjectionLayer layer) => _layer = layer;

    public PhaseOrder Order => PhaseOrder.Earliest;

    public bool CanInitialize(ApplicationContext context) =>
        context.Has<IServiceCollection>();

    public void Initialize(ApplicationContext context) { }
}
