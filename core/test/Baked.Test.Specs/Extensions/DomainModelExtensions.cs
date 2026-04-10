using Baked.Domain.Model;
using Baked.Testing;

using static Baked.Domain.Export.AttributeExport;

namespace Baked.Test;

public static class DomainModelExtensions
{
    extension(Stubber _)
    {
        public AttributeCollection AnAttributeCollection(
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

    extension(List<AttributeFilter> filters)
    {
        public void ShouldContain<T>() where T : Attribute
        {
            filters.ShouldContain(f => f.Type == typeof(T));
        }

        public void ShouldNotContain<T>() where T : Attribute
        {
            filters.ShouldNotContain(f => f.Type == typeof(T));
        }
    }
}