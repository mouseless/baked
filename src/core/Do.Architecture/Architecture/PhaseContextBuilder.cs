namespace Do.Architecture;

public class PhaseContextBuilder
{
    readonly List<ConfigurationTarget> _targets = new();
    Action? _onDispose;

    public PhaseContextBuilder Add<TTarget>(TTarget target) { _targets.Add(ConfigurationTarget.Create(target)); return this; }
    public PhaseContextBuilder Add<TTarget1, TTarget2>(TTarget1 target1, TTarget2 target2) { _targets.Add(ConfigurationTarget.Create((target1, target2))); return this; }
    public PhaseContextBuilder Add<TTarget1, TTarget2, TTarget3>(TTarget1 target1, TTarget2 target2, TTarget3 target3) { _targets.Add(ConfigurationTarget.Create((target1, target2, target3))); return this; }

    public PhaseContextBuilder OnDispose(Action? onDispose) { _onDispose = onDispose; return this; }

    public PhaseContext Build() => new(_targets) { OnDispose = _onDispose };
}

public static class PhaseContextBuilderExtensions
{
    public static PhaseContextBuilder CreateContextBuilder(this IPhase source) => new();

    public static PhaseContext CreateContext<TTarget>(this IPhase source, TTarget target,
        Action? onDispose = default
    ) => source.CreateContextBuilder().Add(target).OnDispose(onDispose).Build();

    public static PhaseContext CreateContext<TTarget1, TTarget2>(this IPhase source, TTarget1 target1, TTarget2 target2,
        Action? onDispose = default
    ) => source.CreateContextBuilder().Add(target1, target2).OnDispose(onDispose).Build();

    public static PhaseContext CreateContext<TTarget1, TTarget2, TTarget3>(this IPhase source, TTarget1 target1, TTarget2 target2, TTarget3 target3,
        Action? onDispose = default
    ) => source.CreateContextBuilder().Add(target1, target2, target3).OnDispose(onDispose!).Build();
}
