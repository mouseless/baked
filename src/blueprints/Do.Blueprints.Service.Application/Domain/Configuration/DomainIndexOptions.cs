using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class DomainIndexOptions
{
    public List<IModelCollectionIndex> Type { get; } = [];
    public List<IModelCollectionIndex> MethodGroup { get; } = [];
    public List<IModelCollectionIndex> Parameters { get; } = [];
    public List<IModelCollectionIndex> Property { get; } = [];
}