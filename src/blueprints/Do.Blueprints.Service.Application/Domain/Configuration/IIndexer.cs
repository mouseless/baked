using Do.Domain.Model;

namespace Do.Domain.Configuration;

public interface IIndexer
{
    bool AppliestTo(IModel model);
    void Apply<T>(IIndexedCollection<T> index, T model) where T : IModel;
}
