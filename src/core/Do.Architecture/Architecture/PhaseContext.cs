namespace Do.Architecture;

public class PhaseContext : IDisposable
{
    public static readonly PhaseContext Empty = new(Enumerable.Empty<ConfigurationTarget>());

    public IEnumerable<ConfigurationTarget> Configurators { get; }
    public Action? OnDispose { get; init; }

    public PhaseContext(IEnumerable<ConfigurationTarget> configurators) => Configurators = new List<ConfigurationTarget>(configurators);

    void IDisposable.Dispose() => OnDispose?.Invoke();
}
