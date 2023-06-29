namespace Do.Architecture;

public interface ILayer
{
    IEnumerable<IPhase> GetPhases();
    ConfigurationTarget GetConfigurationTarget(ApplicationContext context);
}
