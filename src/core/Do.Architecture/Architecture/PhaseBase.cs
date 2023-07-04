namespace Do.Architecture;

public abstract class PhaseBase : IPhase
{
    protected virtual PhaseOrder Order => PhaseOrder.Normal;
    protected virtual void Initialize(ApplicationContext context) { }

    PhaseOrder IPhase.Order => Order;
    bool IPhase.CanInitialize(ApplicationContext context) => true;
    void IPhase.Initialize(ApplicationContext context) => Initialize(context);
}
