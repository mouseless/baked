namespace Do.Domain.Model;

public class AttributeCollection : IEnumerable<(Type Type, HashSet<Attribute> Attributes)>
{
    readonly Dictionary<Type, HashSet<Attribute>> _attributes = [];

    internal AttributeCollection(IEnumerable<Attribute> attributes)
    {
        foreach (var attribute in attributes)
        {
            Add(attribute);
        }
    }

    internal void Add(Attribute attribute)
    {
        var type = attribute.GetType();
        if (!_attributes.ContainsKey(type))
        {
            _attributes[type] = [];
        }

        _attributes[type].Add(attribute);
    }

    public bool ContainsKey<T>() =>
        _attributes.ContainsKey(typeof(T));

    public bool ContainsKey(Type type) =>
        _attributes.ContainsKey(type);

    public bool Contains(Attribute attribute) =>
        _attributes.TryGetValue(attribute.GetType(), out var list) && list.Contains(attribute);

    public IEnumerator<(Type Type, HashSet<Attribute> Attributes)> GetEnumerator() =>
        new Enumerator(_attributes.GetEnumerator());

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    class Enumerator : IEnumerator<(Type Type, HashSet<Attribute> Attributes)>
    {
        readonly IEnumerator<KeyValuePair<Type, HashSet<Attribute>>> _real;

        public Enumerator(IEnumerator<KeyValuePair<Type, HashSet<Attribute>>> real) =>
            _real = real;

        public (Type Type, HashSet<Attribute> Attributes) Current => (_real.Current.Key, _real.Current.Value);

        object IEnumerator.Current => Current;

        public void Dispose() => _real.Dispose();
        public bool MoveNext() => _real.MoveNext();
        public void Reset() => _real.Reset();
    }
}

