namespace Do.Architecture;

public class PhaseContext : IDisposable
{
    static readonly PhaseContext _empty = new(Enumerable.Empty<LayerConfigurator>());

    public PhaseContext(IEnumerable<LayerConfigurator> configurators) => Configurators = new List<LayerConfigurator>(configurators);

    public IEnumerable<LayerConfigurator> Configurators { get; }
    public Action? OnDispose { get; init; }

    public static PhaseContext Empty => _empty;

    void IDisposable.Dispose() => OnDispose?.Invoke();
}
