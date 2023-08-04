using Do.Architecture;
using Do.Database;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace Do;

public static class DatabaseExtensions
{
    public static void AddDatabase(this List<IFeature> source, Func<DatabaseConfigurator, IFeature> configure,
        Func<DatabaseConfigurator, IFeature>? configureDevelopment = default
    )
    {
        source.Add(configure(new()));

        if (configureDevelopment is not null)
        {
            source.Add(configureDevelopment(new()));
        }
    }

    public static PropertyPart Index<TEntity>(this PropertyPart source, string name) => source.Index($"IX_{typeof(TEntity).Name}_{name}");
    public static void Index(this IManyToOneInstance source, Type entity, string name) => source.Index($"IX_{entity.Name}_{name}");
}
