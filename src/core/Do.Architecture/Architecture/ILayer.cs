namespace Do.Architecture;

public interface ILayer
{
    string Id => GetType().Name;
    IEnumerable<IPhase> GetPhases();
    PhaseContext GetContext(IPhase phase, ApplicationContext context);
}
