namespace Do.Architecture;

public class PhaseContext(IEnumerable<LayerConfigurator> configurators)
    : IDisposable
{
    public static readonly PhaseContext Empty = new(Enumerable.Empty<LayerConfigurator>());

    public IEnumerable<LayerConfigurator> Configurators { get; } = new List<LayerConfigurator>(configurators);
    public Action? OnDispose { get; init; }

    void IDisposable.Dispose() => OnDispose?.Invoke();
}
