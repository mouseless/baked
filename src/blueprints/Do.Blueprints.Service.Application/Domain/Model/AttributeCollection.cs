namespace Do.Domain.Model;

public class AttributeCollection() : Dictionary<Type, List<Attribute>>
{
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
        if (!ContainsKey(type))
        {
            this[type] = [];
        }

        this[type].Add(attribute);
    }
}

