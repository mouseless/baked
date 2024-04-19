using Do.Architecture;
using Do.Domain;
using Do.Domain.Configuration;
using Do.Domain.Conventions;
using Do.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Do;

public static class DomainExtensions
{
    public static void AddDomain(this ICollection<ILayer> layers) =>
        layers.Add(new DomainLayer());

    public static IDomainTypeCollection GetDomainTypes(this ApplicationContext source) =>
        source.Get<IDomainTypeCollection>();

    public static DomainModel GetDomainModel(this ApplicationContext source) =>
        source.Get<DomainModel>();

    public static void ConfigureDomainTypeCollection(this LayerConfigurator configurator, Action<IDomainTypeCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureDomainModelBuilder(this LayerConfigurator configurator, Action<DomainModelBuilderOptions> configuration) =>
        configurator.Configure(configuration);

    public static void Add<T>(this ICollection<Type> types) =>
        types.Add(typeof(T));

    public static string GetCSharpFriendlyFullName(this Type type) =>
        !type.IsGenericType ? type.FullName ?? type.Name :
        type.GetGenericTypeDefinition() == typeof(Nullable<>) ? $"{type.GenericTypeArguments.First().GetCSharpFriendlyFullName()}?" :
        $"{type.Namespace}.{type.Name[..type.Name.IndexOf("`")]}<{string.Join(", ", type.GenericTypeArguments.Select(GetCSharpFriendlyFullName))}>";

    public static void Add(this ICollection<TypeBuildLevelFilter> filters, TypeModel.Factory buildLevel) =>
        filters.Add((Type _) => true, buildLevel);

    public static void Add(this ICollection<TypeBuildLevelFilter> filters, Func<Type, bool> filter, TypeModel.Factory buildLevel) =>
        filters.Add(context => filter(context.Type), buildLevel);

    public static void Add(this ICollection<TypeBuildLevelFilter> filters, Func<TypeModelBuildContext, bool> filter, TypeModel.Factory buildLevel) =>
        filters.Add(new(filter, buildLevel));

    public static void Apply(this IEnumerable<TypeModelReference> references, Action<Type> action)
    {
        foreach (var reference in references)
        {
            reference.Apply(action);
        }
    }

    public static bool Contains(this ModelCollection<TypeModelReference> source, Type type) =>
        source.Contains(TypeModelReference.IdFrom(type));

    public static bool Contains(this ModelCollection<TypeModelReference> source, TypeModel type) =>
        source.Contains(((IModel)type).Id);

    public static bool Contains(this ModelCollection<TypeModel> source, Type type) =>
        source.Contains(TypeModelReference.IdFrom(type));

    public static bool Has<T>(this ICustomAttributesModel model) where T : Attribute =>
        model.CustomAttributes.Contains<T>();

    public static T GetSingle<T>(this ICustomAttributesModel model) where T : Attribute =>
        model.Get<T>().Single();

    public static IEnumerable<T> Get<T>(this ICustomAttributesModel model) where T : Attribute =>
        model.CustomAttributes.Get<T>();

    public static bool TryGetSingle<T>(this ICustomAttributesModel model, [NotNullWhen(true)] out T? result) where T : Attribute
    {
        if (!model.TryGet<T>(out var attributes))
        {
            result = null;

            return false;
        }

        result = attributes.SingleOrDefault();

        return result is not null;
    }

    public static bool TryGet<T>(this ICustomAttributesModel model, [NotNullWhen(true)] out IEnumerable<T>? result) where T : Attribute =>
        model.CustomAttributes.TryGet(out result);

    #region IDomainModelConvention

    public static void AddTypeMetadata(this ICollection<IDomainModelConvention> source, Attribute attribute, Func<TypeModelMetadataContext, bool> when,
        int? order = default
    ) => source.AddTypeMetadata((context, add) => add(context.Type, attribute), when, order);

    public static void AddTypeMetadata(this ICollection<IDomainModelConvention> source, Attribute[] attributes, Func<TypeModelMetadataContext, bool> when,
        int? order = default
    ) => source.AddTypeMetadata((context, add) => Array.ForEach(attributes, a => add(context.Type, a)), when, order);

    public static void AddTypeMetadata(this ICollection<IDomainModelConvention> source, Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
        int? order = default
    ) => source.Add(new MetadataConvention<TypeModelMetadataContext>(apply, when, order));

    public static void AddPropertyMetadata(this ICollection<IDomainModelConvention> source, Attribute attribute, Func<PropertyModelContext, bool> when,
        int? order = default
    ) => source.AddPropertyMetadata((context, add) => add(context.Property, attribute), when, order);

    public static void AddPropertyMetadata(this ICollection<IDomainModelConvention> source, Attribute[] attributes, Func<PropertyModelContext, bool> when,
        int? order = default
    ) => source.AddPropertyMetadata((context, add) => Array.ForEach(attributes, a => add(context.Property, a)), when, order);

    public static void AddPropertyMetadata(this ICollection<IDomainModelConvention> source, Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
        int? order = default
    ) => source.Add(new MetadataConvention<PropertyModelContext>(apply, when, order));

    public static void AddMethodMetadata(this ICollection<IDomainModelConvention> source, Attribute attribute, Func<MethodModelContext, bool> when,
        int? order = default
    ) => source.AddMethodMetadata((context, add) => add(context.Method, attribute), when, order);

    public static void AddMethodMetadata(this ICollection<IDomainModelConvention> source, Attribute[] attributes, Func<MethodModelContext, bool> when,
        int? order = default
    ) => source.AddMethodMetadata((context, add) => Array.ForEach(attributes, a => add(context.Method, a)), when, order);

    public static void AddMethodMetadata(this ICollection<IDomainModelConvention> source, Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
        int? order = default
    ) => source.Add(new MetadataConvention<MethodModelContext>(apply, when, order));

    public static void AddParameterMetadata(this ICollection<IDomainModelConvention> source, Attribute attribute, Func<ParameterModelContext, bool> when,
        int? order = default
    ) => source.AddParameterMetadata((context, add) => add(context.Parameter, attribute), when, order);

    public static void AddParameterMetadata(this ICollection<IDomainModelConvention> source, Attribute[] attributes, Func<ParameterModelContext, bool> when,
        int? order = default
    ) => source.AddParameterMetadata((context, add) => Array.ForEach(attributes, a => add(context.Parameter, a)), when, order);

    public static void AddParameterMetadata(this ICollection<IDomainModelConvention> source, Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
        int? order = default
    ) => source.Add(new MetadataConvention<ParameterModelContext>(apply, when, order));

    #endregion

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