namespace Do.Architecture;

public class Layers
{
    internal List<ILayer> List { get; } = new();

    public Layers Add(ILayer layer)
    {
        List.Add(layer);

        return this;
    }
}
