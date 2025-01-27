namespace Baked.Architecture;

public interface ILayer
{
    string Id { get; }
    IEnumerable<IPhase> GetPhases();
    IEnumerable<IPhase> GetBakePhases();
    PhaseContext GetContext(IPhase phase, ApplicationContext context);
}