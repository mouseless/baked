namespace Do.Architecture;

public abstract class PhaseBase : IPhase
{
    readonly PhaseOrder _order;

    protected PhaseBase(PhaseOrder order = PhaseOrder.Normal) => _order = order;

    protected virtual ApplicationContext Context { get; private set; } = default!;

    protected virtual bool IsReady => true;
    protected virtual void Initialize() { }

    ApplicationContext IPhase.Context { get => Context; set => Context = value; }
    bool IPhase.IsReady => IsReady;
    PhaseOrder IPhase.Order => _order;

    void IPhase.Initialize() => Initialize();
}

public abstract class PhaseBase<T> : PhaseBase
{
    protected PhaseBase(PhaseOrder order = PhaseOrder.Normal)
        : base(order: order) { }

    protected override sealed bool IsReady => Context.Has<T>();
    protected override sealed void Initialize() => Initialize(Context.Get<T>());

    protected abstract void Initialize(T dependency);
}

public abstract class PhaseBase<T1, T2> : PhaseBase
{
    protected PhaseBase(PhaseOrder order = PhaseOrder.Normal)
        : base(order: order) { }

    protected override sealed bool IsReady => Context.Has<T1>() && Context.Has<T2>();
    protected override sealed void Initialize() => Initialize(Context.Get<T1>(), Context.Get<T2>());

    protected abstract void Initialize(T1 dependency1, T2 dependency2);
}

public abstract class PhaseBase<T1, T2, T3> : PhaseBase
{
    protected PhaseBase(PhaseOrder order = PhaseOrder.Normal)
        : base(order: order) { }

    protected override sealed bool IsReady => Context.Has<T1>() && Context.Has<T2>() && Context.Has<T3>();
    protected override sealed void Initialize() => Initialize(Context.Get<T1>(), Context.Get<T2>(), Context.Get<T3>());

    protected abstract void Initialize(T1 dependency1, T2 dependency2, T3 dependency3);
}
