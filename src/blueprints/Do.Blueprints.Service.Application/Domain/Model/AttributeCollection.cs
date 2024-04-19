using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class AttributeCollection
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

    public bool Contains<T>() where T : Attribute =>
        Contains(typeof(T));

    public bool Contains(Type type) =>
        _attributes.ContainsKey(type);

    public IEnumerable<T> Get<T>() where T : Attribute =>
        Get(typeof(T)).Cast<T>();

    public IEnumerable<Attribute> Get(Type type) =>
        _attributes[type];

    public bool TryGet<T>([NotNullWhen(true)] out IEnumerable<T>? result) where T : Attribute
    {
        if (!_attributes.TryGetValue(typeof(T), out var set))
        {
            result = null;

            return false;
        }

        result = set.Cast<T>();

        return true;
    }
}