
namespace Do.Domain.Model;

public class DomainIndexer(List<AttributeIndexer> _indexers)
{
    IndexerCollection Indexers { get; } = new(_indexers);

    public void Execute(DomainModel model)
    {
        Indexers.Apply(model.Types);

        foreach (var methods in model.Types.Select(t => t.Methods))
        {
            Indexers.Apply(methods);
        }
    }
}
