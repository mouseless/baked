using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Model;

public class AttributeCollection()
    : IMutableAttributeCollection
{
    readonly Dictionary<Type, List<Attribute>> _attributes = [];

    internal AttributeCollection(IEnumerable<Attribute> attributes) : this()
    {
        foreach (var attribute in attributes)
        {
            Add(attribute);
        }
    }

    void Add(Attribute attribute)
    {
        var type = attribute.GetType();
        if (!_attributes.ContainsKey(type))
        {
            _attributes[type] = [];
        }

        if (!attribute.AllowsMultiple())
        {
            _attributes[type].Clear();
        }

        _attributes[type].Add(attribute);
    }

    void Remove(Type type)
    {
        if (!_attributes.ContainsKey(type)) { return; }

        _attributes.Remove(type);
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

    void IMutableAttributeCollection.Add(Attribute attribute) =>
        Add(attribute);

    void IMutableAttributeCollection.Remove(Type type) =>
        Remove(type);
}