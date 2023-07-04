using Do.Architecture;
using Do.Test.Blueprints.Service.DependencyInjection.Phases;

namespace Do.Test.Blueprints.Service.DependencyInjection;

public class DependencyInjectionLayer : LayerBase
{
    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new AddServices();
    }

    protected override ConfigurationTarget GetConfigurationTarget(IPhase phase, ApplicationContext context) =>
        phase switch
        {
            AddServices => ConfigurationTarget.Create(context.Get<IServiceCollection>()),
            _ => ConfigurationTarget.Empty
        };
}
