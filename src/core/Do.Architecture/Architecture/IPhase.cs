namespace Do.Architecture;

public interface IPhase
{
    ApplicationContext ApplicationContext { get; }
    PhaseOrder Order { get; }

    bool IsReady(ApplicationContext context);
    void Initialize(ApplicationContext context);
}
