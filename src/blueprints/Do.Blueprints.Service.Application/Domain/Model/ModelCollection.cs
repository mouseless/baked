using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T>() : IEnumerable<T>
    where T : IModel
{
    public static implicit operator ModelCollection<T>(List<T> other) => new(other);

    readonly Dictionary<string, T> _models = [];

    public ModelCollection(List<T> data)
        : this() => _models = data.ToDictionary(m => m.Id, m => m);

    List<T> List => _models.Values.ToList();

    public int Count => _models.Count;

    public T this[int index] => List[index];

    public void Add(T model) => _models[model.Id] = model;

    public T? GetOrDefault(string id) => _models.ContainsKey(id) ? _models[id] : default;

    internal bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
        _models.TryGetValue(id, out model);

    public IEnumerator<T> GetEnumerator() => new ModelCollectionEnumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public class ModelCollectionEnumerator(ModelCollection<T> collection) : IEnumerator<T>, IEnumerator
    {
        readonly ModelCollection<T> _collection = collection;
        int _index = 0;
        T _current = default!;

        public T Current => _current;

        object IEnumerator.Current => Current;

        public void Dispose() { }

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
    }
}
