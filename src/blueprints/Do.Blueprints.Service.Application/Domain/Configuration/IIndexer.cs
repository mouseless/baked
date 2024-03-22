using Do.Domain.Model;

namespace Do.Domain.Configuration;

public interface IIndexer
{
    void Execute<T>(ModelIndex<T> index, T model) where T : IModel;
}
