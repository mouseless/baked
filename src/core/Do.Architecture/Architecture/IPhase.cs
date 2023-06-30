namespace Do.Architecture;

public interface IPhase
{
    PhaseOrder Order { get; }

    bool CanInitialize(ApplicationContext context);
    void Initialize(ApplicationContext context);
}
