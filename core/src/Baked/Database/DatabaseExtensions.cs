using Baked.Architecture;
using Baked.Database;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace Baked;

public static class DatabaseExtensions
{
    extension(List<IFeature> features)
    {
        public void AddDatabase(FeatureFunc<DatabaseConfigurator> configure) =>
            features.Add(configure(new()));
    }

    extension(PropertyPart propertyPart)
    {
        public PropertyPart Index<TEntity>(string name) =>
            propertyPart.Index($"IX_{typeof(TEntity).Name}_{name}");
    }

    extension(IManyToOneInstance manyToOne)
    {
        public void Index(Type entity, string name) =>
            manyToOne.Index($"IX_{entity.Name}_{name}");
    }
}