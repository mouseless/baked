using Do.Architecture;
using Do.Domain;
using Do.Domain.Configuration;
using Do.Domain.Model;

namespace Do;

public static class DomainExtensions
{
    public static void AddDomain(this ICollection<ILayer> layers) => layers.Add(new DomainLayer());

    public static DomainModel GetDomainModel(this ApplicationContext source) => source.Get<DomainModel>();

    public static void ConfigureDomainTypeCollection(this LayerConfigurator configurator, Action<IDomainTypeCollection> configuration) => configurator.Configure(configuration);
    public static void ConfigureDomainModelBuilder(this LayerConfigurator configurator, Action<DomainModelBuilderOptions> configuration) => configurator.Configure(configuration);

    public static ICollection<MetadataConvention<TypeModel>> Add(this ICollection<MetadataConvention<TypeModel>> source, Attribute attribute, Func<TypeModelMetadata, bool> when,
        int? order = default
    ) => source.Add((model, add) => add(model, attribute), when, order);

    public static ICollection<MetadataConvention<TypeModel>> Add<TAttribute>(this ICollection<MetadataConvention<TypeModel>> source, Attribute[] attributes, Func<TypeModelMetadata, bool> when,
        int? order = default
    ) => source.Add((model, add) => Array.ForEach(attributes, a => add(model, a)), when, order);

    public static ICollection<MetadataConvention<TypeModel>> Add(this ICollection<MetadataConvention<TypeModel>> source, Action<TypeModelMetadata, Action<IMemberModel, Attribute>> apply, Func<TypeModelMetadata, bool> when,
        int? order = default
    )
    {
        source.Add(new MetadataConvention<TypeModel>(
            t => t.TryGetMetadata(out var metadata) && when(metadata),
            (t, add) => apply(t.GetMetadata(), add),
            _order: order
        ));

        return source;
    }

    public static ICollection<MetadataConvention<T>> Add<T>(this ICollection<MetadataConvention<T>> source, Attribute attribute, Func<T, bool> when,
        int? order = default
    ) where T : IMemberModel =>
        source.Add((model, add) => add(model, attribute), when, order);

    public static ICollection<MetadataConvention<T>> Add<T, TAttribute>(this ICollection<MetadataConvention<T>> source, Attribute[] attributes, Func<T, bool> when,
        int? order = default
    ) where T : IMemberModel =>
        source.Add((model, add) => Array.ForEach(attributes, a => add(model, a)), when, order);

    public static ICollection<MetadataConvention<T>> Add<T>(this ICollection<MetadataConvention<T>> source, Action<T, Action<IMemberModel, Attribute>> apply, Func<T, bool> when,
        int? order = default
    ) where T : IModel
    {
        source.Add(new(when, apply, _order: order));

        return source;
    }

    public static void Add<T>(this ICollection<Type> types) =>
        types.Add(typeof(T));

    public static void Add(this List<TypeBuildLevelFilter> filters, Func<Type, bool> filter, TypeModel.Factory buildLevel) =>
        filters.Add(context => filter(context.Type), buildLevel);

    public static void Add(this List<TypeBuildLevelFilter> filters, Func<TypeModelBuildContext, bool> filter, TypeModel.Factory buildLevel) =>
        filters.Add(new(filter, buildLevel));

    public static void Apply(this IEnumerable<TypeModelReference> references, Action<Type> action)
    {
        foreach (var reference in references)
        {
            reference.Apply(action);
        }
    }

    public static void Apply(this IEnumerable<TypeModel> types, Action<Type> action)
    {
        foreach (var type in types)
        {
            type.Apply(action);
        }
    }

    public static bool Contains(this ModelCollection<TypeModelReference> models, Type type) =>
        models.Contains(TypeModelReference.IdFrom(type));

    public static bool Contains(this ModelCollection<TypeModelReference> models, TypeModel type) =>
        models.Contains(((IModel)type).Id);

    public static bool Contains(this ModelCollection<TypeModel> models, Type type) =>
        models.Contains(TypeModelReference.IdFrom(type));
}
