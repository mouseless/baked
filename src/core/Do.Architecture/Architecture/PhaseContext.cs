namespace Do.Architecture;

public class PhaseContext : IDisposable
{
    public static readonly PhaseContext Empty = new(ConfigurationTarget.Empty);

    public ConfigurationTarget ConfigurationTarget { get; }
    public Action? OnDispose { get; init; }

    public PhaseContext(ConfigurationTarget configurationTarget) => ConfigurationTarget = configurationTarget;

    void IDisposable.Dispose() => OnDispose?.Invoke();
}

public static class PhaseContextExtensions
{
    public static PhaseContext CreateContext<TTarget>(this IPhase source, TTarget target,
        Action? onDispose = default
    ) => new(ConfigurationTarget.Create(target)) { OnDispose = onDispose };

    public static PhaseContext CreateContext<TTarget1, TTarget2>(this IPhase source, TTarget1 target1, TTarget2 target2,
        Action? onDispose = default
    ) => new(ConfigurationTarget.Create((target1, target2))) { OnDispose = onDispose };

    public static PhaseContext CreateContext<TTarget1, TTarget2, TTarget3>(this IPhase source, TTarget1 target1, TTarget2 target2, TTarget3 target3,
        Action? onDispose = default
    ) => new(ConfigurationTarget.Create((target1, target2, target3))) { OnDispose = onDispose };
}

public class PhaseContextBuilder
{
    public PhaseContextBuilder Add<TTarget>(TTarget target) => this;
    public PhaseContextBuilder Add<TTarget1, TTarget2>(TTarget1 target1, TTarget2 target2) => this;
    public PhaseContextBuilder Add<TTarget1, TTarget2, TTarget3>(TTarget1 target1, TTarget2 target2, TTarget3 target3) => this;

    public PhaseContextBuilder OnDispose(Action onDispose) => this;

    public PhaseContext Build() => throw new NotImplementedException();
}

public static class PhaseContextBuilderExtensions
{
    public static PhaseContextBuilder CreateContextBuilder(this IPhase source) => new();
}
