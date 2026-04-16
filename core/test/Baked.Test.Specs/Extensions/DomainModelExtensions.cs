using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Export;
using Baked.Domain.Model;
using Baked.Testing;

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

        public DomainModelBuilder ADomainModelBuilder(
            Action<DomainModelBuilderOptions>? options = default
        )
        {
            var optionsInstance = new DomainModelBuilderOptions();
            optionsInstance.BuildLevels.Add(BuildLevels.Metadata);

            if (options is not null)
            {
                options(optionsInstance);
            }

            return new DomainModelBuilder(optionsInstance);
        }
    }

    extension(DomainModelBuilder builder)
    {
        public DomainModel Build(IEnumerable<Type> types)
        {
            var collection = new DomainTypeCollection();
            collection.AddRange(types);

            return builder.Build(collection);
        }
    }

    extension(List<IAttributeExport> filters)
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