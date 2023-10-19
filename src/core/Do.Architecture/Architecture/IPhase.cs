namespace Do.Architecture;

public interface IPhase
{
    ApplicationContext Context { get; internal set; }
    bool IsReady { get; }
    PhaseOrder Order { get; }

    void Initialize();
}
