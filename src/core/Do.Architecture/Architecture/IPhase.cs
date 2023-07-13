namespace Do.Architecture;

public interface IPhase
{
    PhaseOrder Order { get; }

    bool IsReady(ApplicationContext context);
    void Initialize(ApplicationContext context);
}
