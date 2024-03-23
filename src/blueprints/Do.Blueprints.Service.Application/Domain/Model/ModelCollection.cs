using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T> : IEnumerable<T>, IIndexedCollection<T>
    where T : IModel
{
    readonly KeyedModelCollection<T> _models = [];
    readonly ModelIndex<T> _index = [];

    public ModelCollection() { }

    public ModelCollection(IEnumerable<T> models)
    {
        foreach (var model in models)
        {
            _models.Add(model);
        }
    }

    public T this[string id] =>
        _models[id];

    public int Count => _models.Count;

    public bool ContainsModel(T? model) =>
        _models.Contains(model?.Id ?? string.Empty);

    public bool Contains(string id) =>
        _models.Contains(id);

    public bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
       _models.TryGetValue(id, out model);

    internal bool TryAdd(T typeModel)
    {
        if (!ContainsModel(typeModel))
        {
            _models.Add(typeModel);

            return true;
        }

        return false;
    }

    public ModelCollection<T> GetIndex(string id) =>
        _index.GetOrEmpty(id);

    public ModelCollection<T> WithAttribute<TAttribute>() where TAttribute : Attribute =>
        GetIndex(TypeModel.IdFrom(typeof(TAttribute)));

    public IEnumerator<T> GetEnumerator() => _models.GetEnumerator();

    ModelIndex<T> IIndexedCollection<T>.Index => _index;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public interface IIndexedCollection<T> where T : IModel
{
    ModelIndex<T> Index { get; }
}
