namespace Baked.Architecture;

public interface ILayer
{
    string Id { get; }
    IEnumerable<IPhase> GetPhases();
    PhaseContext GetContext(IPhase phase, ApplicationContext context);
}