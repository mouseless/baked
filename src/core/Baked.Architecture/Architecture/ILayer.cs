namespace Baked.Architecture;

public interface ILayer
{
    string Id { get; }
    IEnumerable<IPhase> GetPhases();
    IEnumerable<IPhase> GetGeneratePhases();
    PhaseContext GetContext(IPhase phase, ApplicationContext context);
}