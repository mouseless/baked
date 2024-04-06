namespace Do.Architecture;

public interface IPhase
{
    ApplicationContext Context { get; protected internal set; }
    bool IsReady { get; }
    PhaseOrder Order { get; }

    void Initialize();
}