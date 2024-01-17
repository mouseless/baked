using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T>() : IEnumerable<T>
    where T : IModel
{
    public static implicit operator ModelCollection<T>(List<T> other) => new(other);

    readonly KeyedModelCollection<T> _models = [];

    public ModelCollection(List<T> data)
        : this() => AddRange(data);

    public int Count => _models.Count;

    public T this[int index] =>
        _models[index];

    public void Add(T model) =>
        _models.Add(model);

    public bool TryAdd(T model)
    {
        if (_models.Contains(model.Id)) { return false; }

        _models.Add(model);

        return true;
    }

    public void AddRange(IEnumerable<T> models)
    {
        foreach (var model in models)
        {
            _models.Add(model);
        }
    }

    public bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
        _models.TryGetValue(id, out model);

    public IEnumerator<T> GetEnumerator() => new ModelCollectionEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct ModelCollectionEnumerator(ModelCollection<T> collection) : IEnumerator<T>, IEnumerator
    {
        readonly ModelCollection<T> _collection = collection;
        int _index = 0;
        T _current = default!;

        public T Current => _current;
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_index < _collection.Count)
            {
                _current = _collection[_index];
                _index++;

                return true;
            }

            _current = default!;
            _index++;

            return false;
        }

        public void Reset()
        {
            _index = 0;
            _current = default!;
        }

        public void Dispose() { }
    }

    class KeyedModelCollection<TItem> : KeyedCollection<string, TItem>
        where TItem : IModel
    {
        protected override string GetKeyForItem(TItem item) => item.Id;
    }
}
