using Do.Architecture;
using Do.Domain;
using Do.Domain.Configuration;
using Do.Domain.Model;
using System.Diagnostics.CodeAnalysis;

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

    public static ICollection<MetadataConvention<TypeModel>> Add(this ICollection<MetadataConvention<TypeModel>> source, Action<TypeModelMetadata, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadata, bool> when,
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
    ) where T : IModel, ICustomAttributesModel =>
        source.Add((model, add) => add(model, attribute), when, order);

    public static ICollection<MetadataConvention<T>> Add<T, TAttribute>(this ICollection<MetadataConvention<T>> source, Attribute[] attributes, Func<T, bool> when,
        int? order = default
    ) where T : IModel, ICustomAttributesModel =>
        source.Add((model, add) => Array.ForEach(attributes, a => add(model, a)), when, order);

    public static ICollection<MetadataConvention<T>> Add<T>(this ICollection<MetadataConvention<T>> source, Action<T, Action<ICustomAttributesModel, Attribute>> apply, Func<T, bool> when,
        int? order = default
    ) where T : IModel, ICustomAttributesModel
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

    public static bool Contains(this ModelCollection<TypeModelReference> source, Type type) =>
        source.Contains(TypeModelReference.IdFrom(type));

    public static bool Contains(this ModelCollection<TypeModelReference> source, TypeModel type) =>
        source.Contains(((IModel)type).Id);

    public static bool Contains(this ModelCollection<TypeModel> source, Type type) =>
        source.Contains(TypeModelReference.IdFrom(type));

    #region TypeModel

    public static bool HasGenerics(this TypeModel type) =>
        type.HasInfo<TypeModelGenerics>();

    public static TypeModelGenerics GetGenerics(this TypeModel type) =>
        type.GetInfo<TypeModelGenerics>();

    public static bool TryGetGenerics(this TypeModel type, [NotNullWhen(true)] out TypeModelGenerics? result) =>
        type.TryGetInfo(out result);

    public static bool HasInheritance(this TypeModel type) =>
        type.HasInfo<TypeModelInheritance>();

    public static TypeModelInheritance GetInheritance(this TypeModel type) =>
        type.GetInfo<TypeModelInheritance>();

    public static bool TryGetInheritance(this TypeModel type, [NotNullWhen(true)] out TypeModelInheritance? result) =>
        type.TryGetInfo(out result);

    public static bool HasMetadata(this TypeModel type) =>
        type.HasInfo<TypeModelMetadata>();

    public static TypeModelMetadata GetMetadata(this TypeModel type) =>
        type.GetInfo<TypeModelMetadata>();

    public static bool TryGetMetadata(this TypeModel type, [NotNullWhen(true)] out TypeModelMetadata? result) =>
        type.TryGetInfo(out result);

    public static bool HasMembers(this TypeModel type) =>
        type.HasInfo<TypeModelMembers>();

    public static TypeModelMembers GetMembers(this TypeModel type) =>
        type.GetInfo<TypeModelMembers>();

    public static bool TryGetMembers(this TypeModel type, [NotNullWhen(true)] out TypeModelMembers? result) =>
        type.TryGetInfo(out result);

    static bool HasInfo<TInfo>(this TypeModel type) where TInfo : TypeModel =>
        type is TInfo;

    static TInfo GetInfo<TInfo>(this TypeModel type) where TInfo : TypeModel =>
        (TInfo)type;

    static bool TryGetInfo<TInfo>(this TypeModel type, [NotNullWhen(true)] out TInfo? result)
        where TInfo : TypeModel
    {
        result = type as TInfo;

        return result is not null;
    }

    #endregion
}
