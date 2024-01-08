namespace Do.Architecture;

public abstract class PhaseBase(PhaseOrder _order = PhaseOrder.Normal) : IPhase
{
    protected virtual ApplicationContext Context { get; private set; } = default!;

    protected virtual bool IsReady() => true;
    protected virtual void Initialize() { }

    PhaseOrder IPhase.Order => _order;

    bool IPhase.IsReady(ApplicationContext context)
    {
        Context = context;

        return IsReady();
    }

    void IPhase.Initialize(ApplicationContext context)
    {
        Context = context;

        Initialize();
    }
}

public abstract class PhaseBase<T>(PhaseOrder _order = PhaseOrder.Normal)
    : PhaseBase(_order)
{
    protected override sealed bool IsReady() => Context.Has<T>();
    protected override sealed void Initialize() => Initialize(Context.Get<T>());

    protected abstract void Initialize(T dependency);
}

public abstract class PhaseBase<T1, T2>(PhaseOrder _order = PhaseOrder.Normal)
    : PhaseBase(_order)
{
    protected override sealed bool IsReady() => Context.Has<T1>() && Context.Has<T2>();
    protected override sealed void Initialize() => Initialize(Context.Get<T1>(), Context.Get<T2>());

    protected abstract void Initialize(T1 dependency1, T2 dependency2);
}

public abstract class PhaseBase<T1, T2, T3>(PhaseOrder _order = PhaseOrder.Normal)
    : PhaseBase(_order)
{
    protected override sealed bool IsReady() => Context.Has<T1>() && Context.Has<T2>() && Context.Has<T3>();
    protected override sealed void Initialize() => Initialize(Context.Get<T1>(), Context.Get<T2>(), Context.Get<T3>());

    protected abstract void Initialize(T1 dependency1, T2 dependency2, T3 dependency3);
}
