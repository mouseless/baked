using Baked.Architecture;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace Baked;

public static class DatabaseFeature
{
    public static void AddDatabase(this List<IFeature> features,
        FeatureFunc<Configurator> configure
    ) => features.Add(configure(new()));

    public class Configurator : FeatureConfigurator<Configurator>;

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