using Baked.Architecture;
using Baked.Database;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace Baked;

public static class DatabaseExtensions
{
    public static void AddDatabase(this List<IFeature> features, Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>> configure) =>
        features.Add(configure(new()));

    public static PropertyPart Index<TEntity>(this PropertyPart propertyPart, string name) =>
        propertyPart.Index($"IX_{typeof(TEntity).Name}_{name}");

    public static void Index(this IManyToOneInstance manyToOne, Type entity, string name) =>
        manyToOne.Index($"IX_{entity.Name}_{name}");
}