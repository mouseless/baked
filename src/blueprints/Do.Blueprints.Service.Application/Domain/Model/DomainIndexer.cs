
namespace Do.Domain.Model;

public class DomainIndexer(List<IIndexer> _indexers)
{
    IndexerCollection Indexers { get; } = new(_indexers);

    public void Execute(DomainModel model)
    {
        Indexers.Apply(model.Types);

        foreach (var methods in model.Types.Select(t => t.Methods))
        {
            Indexers.Apply(methods);

            foreach (var overloads in methods.Select(m => m.Overloads))
            {
                foreach (var overload in overloads)
                {
                    Indexers.Apply(overload.Parameters);
                }
            }
        }

        foreach (var properties in model.Types.Select(t => t.Properties))
        {
            Indexers.Apply(properties);
        }
    }
}
