namespace Do.Architecture;

public class ApplicationContext
{
    public void Add<T>(T item) { }
    public T Get<T>() => default!;
    public bool Has<T>() => false;
}
