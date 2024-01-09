namespace Do.Architecture;

public class PhaseContext : IDisposable
{
    public static readonly PhaseContext Empty = new(Enumerable.Empty<LayerConfigurator>());

    public IEnumerable<LayerConfigurator> Configurators { get; }
    public Action? OnDispose { get; init; }

    public PhaseContext(IEnumerable<LayerConfigurator> configurators) =>
        Configurators = new List<LayerConfigurator>(configurators);

    void IDisposable.Dispose() => OnDispose?.Invoke();
}
