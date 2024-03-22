namespace Do.Domain.Model;

public interface IIndexer
{
    void Execute<T>(ModelIndex<T> index, T model) where T : IModel;
}
