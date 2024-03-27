
namespace Do.Domain.Model;

public class AttributeCollection() : IEnumerable<KeyValuePair<Type, List<Attribute>>>
{
    readonly Dictionary<Type, List<Attribute>> _attributes = [];

    internal AttributeCollection(IEnumerable<Attribute> attributes)
        : this()
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
        _attributes.TryGetValue(attribute.GetType(), out var list) ? list.Contains(attribute) : false;

    public IEnumerator<KeyValuePair<Type, List<Attribute>>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<Type, List<Attribute>>>)_attributes).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_attributes).GetEnumerator();
}

