using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Model;

public class AttributeCollection(string name)
    : IMutableAttributeCollection
{
    readonly string _name = name;
    readonly Dictionary<Type, List<Attribute>> _attributes = [];

    internal AttributeCollection(string name, IEnumerable<Attribute> attributes) : this(name)
    {
        foreach (var attribute in attributes)
        {
            AddInner(attribute);
        }
    }

    void AddInner(Attribute attribute)
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

    void Set(Attribute attribute)
    {
        if (attribute.AllowsMultiple())
        {
            throw new InvalidOperationException($"`{attribute.GetType().Name}` cannot be set for `{_name}` because it allows multiple. Please use `Add` for this attribute.");
        }

        AddInner(attribute);
    }

    void Add(Attribute attribute)
    {
        if (!attribute.AllowsMultiple())
        {
            throw new InvalidOperationException($"`{attribute.GetType().Name}` cannot be added to `{_name}` because it doesn't allow multiple. Please use `Set` for this attribute.");
        }

        AddInner(attribute);
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

    void IMutableAttributeCollection.Set(Attribute attribute) =>
        Set(attribute);

    void IMutableAttributeCollection.Add(Attribute attribute) =>
        Add(attribute);

    void IMutableAttributeCollection.Remove(Type type) =>
        Remove(type);
}