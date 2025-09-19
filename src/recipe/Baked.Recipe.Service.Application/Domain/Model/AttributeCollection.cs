using System.Diagnostics.CodeAnalysis;

namespace Baked.Domain.Model;

public class AttributeCollection(string name)
    : IMutableAttributeCollection
{
    readonly string _name = name;
    readonly Dictionary<Type, List<Attribute>> _attributes = [];

    internal AttributeCollection(string name, IEnumerable<Attribute> attributes)
        : this(name)
    {
        foreach (var attribute in attributes)
        {
            AddInner(attribute);
        }
    }

    public string Name => _name;

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

    public T Get<T>() where T : Attribute =>
        (T)Get(typeof(T));

    public Attribute Get(Type type)
    {
        if (type.AllowsMultiple())
        {
            throw new InvalidOperationException($"Cannot use `Get` for `{type.Name}` in `{_name}` because it allows multiple. Please use `GetAll` for this attribute.");
        }

        return _attributes[type].Single();
    }

    public bool TryGet<T>([NotNullWhen(true)] out T? result) where T : Attribute
    {
        if (!TryGet(typeof(T), out var attribute))
        {
            result = null;

            return false;
        }

        result = (T)attribute;

        return true;
    }

    public bool TryGet(Type type, [NotNullWhen(true)] out Attribute? result)
    {
        if (type.AllowsMultiple())
        {
            throw new InvalidOperationException($"Cannot use `TryGet` for `{type.Name}` in `{_name}` because it allows multiple. Please use `TryGetAll` for this attribute.");
        }

        if (!_attributes.TryGetValue(type, out var list))
        {
            result = null;

            return false;
        }

        result = list.SingleOrDefault();

        return result is not null;
    }

    public IEnumerable<T> GetAll<T>() where T : Attribute =>
        GetAll(typeof(T)).Cast<T>();

    public IEnumerable<Attribute> GetAll(Type type)
    {
        if (!type.AllowsMultiple())
        {
            throw new InvalidOperationException($"Cannot use `GetAll` for `{type.Name}` in `{_name}` because it doesn't allow multiple. Please use `Get` for this attribute.");
        }

        return _attributes[type].AsReadOnly();
    }

    public bool TryGetAll<T>([NotNullWhen(true)] out IEnumerable<T>? result) where T : Attribute
    {
        if (!TryGetAll(typeof(T), out var list))
        {
            result = null;

            return false;
        }

        result = list.Cast<T>();

        return true;
    }

    public bool TryGetAll(Type type, [NotNullWhen(true)] out IEnumerable<Attribute>? result)
    {
        if (!type.AllowsMultiple())
        {
            throw new InvalidOperationException($"Cannot use `TryGetAll` for `{type.Name}` in `{_name}` because it doesn't allow multiple. Please use `TryGet` for this attribute.");
        }

        if (!_attributes.TryGetValue(type, out var list))
        {
            result = null;

            return false;
        }

        result = list;

        return true;
    }

    void IMutableAttributeCollection.Set(Attribute attribute) =>
        Set(attribute);

    void IMutableAttributeCollection.Add(Attribute attribute) =>
        Add(attribute);

    void IMutableAttributeCollection.Remove(Type type) =>
        Remove(type);
}