namespace Baked.Architecture;

public class PhaseContextBuilder
{
    readonly IPhase _phase = default!;
    readonly List<LayerConfigurator> _configurators = [];
    Action? _onDispose;

    internal PhaseContextBuilder(IPhase phase) => _phase = phase;

    public PhaseContextBuilder Add<TTarget>(TTarget target)
        where TTarget : notnull
    {
        _configurators.Add(LayerConfigurator.Create(_phase.Context, target));

        return this;
    }

    public PhaseContextBuilder Add<TTarget1, TTarget2>(TTarget1 target1, TTarget2 target2)
        where TTarget1 : notnull
        where TTarget2 : notnull
    {
        _configurators.Add(LayerConfigurator.Create(_phase.Context, target1, target2));

        return this;
    }

    public PhaseContextBuilder Add<TTarget1, TTarget2, TTarget3>(TTarget1 target1, TTarget2 target2, TTarget3 target3)
        where TTarget1 : notnull
        where TTarget2 : notnull
        where TTarget3 : notnull
    {
        _configurators.Add(LayerConfigurator.Create(_phase.Context, target1, target2, target3));

        return this;
    }

    public PhaseContextBuilder OnDispose(Action? onDispose)
    {
        _onDispose = onDispose;

        return this;
    }

    public PhaseContext Build() => new(_configurators) { OnDispose = _onDispose };
}

public static class PhaseContextBuilderExtensions
{
    public static PhaseContextBuilder CreateContextBuilder(this IPhase source) => new(source);

    public static PhaseContext CreateContext<TTarget>(this IPhase source, TTarget target,
        Action? onDispose = default
    )
        where TTarget : notnull
    {
        return source.CreateContextBuilder().Add(target).OnDispose(onDispose).Build();
    }

    public static PhaseContext CreateContext<TTarget1, TTarget2>(this IPhase source, TTarget1 target1, TTarget2 target2,
        Action? onDispose = default
    )
        where TTarget1 : notnull
        where TTarget2 : notnull
    {
        return source.CreateContextBuilder().Add(target1, target2).OnDispose(onDispose).Build();
    }

    public static PhaseContext CreateContext<TTarget1, TTarget2, TTarget3>(this IPhase source, TTarget1 target1, TTarget2 target2, TTarget3 target3,
        Action? onDispose = default
    )
        where TTarget1 : notnull
        where TTarget2 : notnull
        where TTarget3 : notnull
    {
        return source.CreateContextBuilder().Add(target1, target2, target3).OnDispose(onDispose).Build();
    }
}