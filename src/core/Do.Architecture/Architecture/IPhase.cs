namespace Do.Architecture;

public interface IPhase
{
    PhaseOrder Order { get; }

    bool Initialize(ApplicationContext context);
}
