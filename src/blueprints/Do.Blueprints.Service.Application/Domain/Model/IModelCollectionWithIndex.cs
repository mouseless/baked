namespace Do.Domain.Model;

public interface IModelCollectionWithIndex<T> : IEnumerable<T> where T : IModel
{
    Dictionary<ModelIndexKey, ModelCollection<T>> Index { get; }
}
