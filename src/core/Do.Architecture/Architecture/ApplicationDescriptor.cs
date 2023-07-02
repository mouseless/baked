namespace Do.Architecture;

public class ApplicationDescriptor
{
    public List<ILayer> Layers { get; } = new();
    public List<IFeature> Features { get; } = new();
}
