namespace Do.Domain.Model;

public interface IModelCollectionWithIndex<T> where T : IModel
{
    internal ModelIndex<T> Index { get; }

    ModelCollection<T> GetIndex(string indexId);
}