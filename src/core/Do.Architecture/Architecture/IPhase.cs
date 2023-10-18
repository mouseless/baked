namespace Do.Architecture;

public interface IPhase
{
    ApplicationContext Context { get; }
    PhaseOrder Order { get; }

    bool IsReady(ApplicationContext context);
    void Initialize(ApplicationContext context);
}
