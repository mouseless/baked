using Do.Architecture;
using Do.Test.Blueprints.Service.DependencyInjection.Phases;

namespace Do.Test.Blueprints.Service.DependencyInjection;

public class DependencyInjectionLayer : ILayer
{
    public IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices();
    }

    public IServiceCollection ServiceCollection { get; internal set; } = default!;

    public ConfigurationTarget GetConfigurationTarget(IPhase phase, ApplicationContext context) =>
        phase switch
        {
            AddServices => ConfigurationTarget.Create(context.Get<IServiceCollection>()),
            _ => ConfigurationTarget.Empty
        };

}
