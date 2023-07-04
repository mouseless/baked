namespace Do.Architecture;

public abstract class LayerBase : ILayer
{
    protected virtual IEnumerable<IPhase> GetPhases() => Enumerable.Empty<IPhase>();
    protected virtual ConfigurationTarget GetConfigurationTarget(IPhase phase, ApplicationContext context) => ConfigurationTarget.Empty;

    IEnumerable<IPhase> ILayer.GetPhases() => GetPhases();
    ConfigurationTarget ILayer.GetConfigurationTarget(IPhase phase, ApplicationContext context) => GetConfigurationTarget(phase, context);
}
