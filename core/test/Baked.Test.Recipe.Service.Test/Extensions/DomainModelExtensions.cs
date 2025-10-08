using Baked.Domain.Model;
using Baked.Testing;

namespace Baked.Test;

public static class DomainModelExtensions
{
    public static AttributeCollection AnAttributeCollection(this Stubber _,
        string? name = default,
        Attribute? item = default,
        IEnumerable<Attribute>? items = default
    )
    {
        name ??= "Test";
        items ??= [];

        var result = new AttributeCollection(name);

        if (item is not null)
        {
            AddOrSet(item);
        }

        foreach (var current in items)
        {
            AddOrSet(current);
        }

        void AddOrSet(Attribute attribute)
        {
            if (attribute.AllowsMultiple())
            {
                ((IMutableAttributeCollection)result).Add(attribute);
            }
            else
            {
                ((IMutableAttributeCollection)result).Set(attribute);
            }

        }

        return result;
    }
}