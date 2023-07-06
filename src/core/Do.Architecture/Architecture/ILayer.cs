namespace Do.Architecture;

public interface ILayer
{
    IEnumerable<IPhase> GetPhases();
    PhaseContext GetContext(IPhase phase, ApplicationContext context);
}
