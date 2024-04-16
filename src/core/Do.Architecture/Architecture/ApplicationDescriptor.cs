namespace Do.Architecture;

public class ApplicationDescriptor
{
    public List<ILayer> Layers { get; } = [];
    public List<IFeature> Features { get; } = [];
}