namespace Do.Architecture;

public class ApplicationContext
{
    public IPhase Phase { get; internal set; } = IPhase.Empty;

    public void Add<T>(T item) { }
    public T Get<T>() => default!;
    public bool Has<T>() => false;
}
