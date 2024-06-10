using Do.Architecture;
using Do.Database;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace Do;

public static class DatabaseExtensions
{
    public static void AddDatabase(this List<IFeature> source, Func<DatabaseConfigurator, IFeature<DatabaseConfigurator>> configure) =>
        source.Add(configure(new()));

    public static PropertyPart Index<TEntity>(this PropertyPart source, string name) =>
        source.Index($"IX_{typeof(TEntity).Name}_{name}");

    public static void Index(this IManyToOneInstance source, Type entity, string name) =>
        source.Index($"IX_{entity.Name}_{name}");
}