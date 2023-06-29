namespace Do.Architecture;

public interface IPhase
{
    public static IPhase Empty => new EmptyPhase();

    PhaseOrder Order { get; }

    bool CanInitialize(ApplicationContext context);
    void Initialize(ApplicationContext context);

    private class EmptyPhase : IPhase
    {
        public PhaseOrder Order => PhaseOrder.Normal;
        public bool CanInitialize(ApplicationContext context) => true;
        public void Initialize(ApplicationContext context) { }
    }
}

public enum PhaseOrder
{
    Earliest = 1,
    Early = 2,
    Normal = 3,
    Late = 4,
    Latest = 5
}
