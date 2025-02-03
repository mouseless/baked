using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Conventions;
using Baked.Domain.Model;
using Baked.Testing;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Baked;

public static class DomainExtensions
{
    public static void AddDomain(this ICollection<ILayer> layers) =>
        layers.Add(new DomainLayer());

    public static IDomainTypeCollection GetDomainTypes(this ApplicationContext application) =>
        application.Get<IDomainTypeCollection>();

    public static DomainModel GetDomainModel(this ApplicationContext context) =>
        context.Get<DomainModel>();

    public static void ConfigureDomainTypeCollection(this LayerConfigurator configurator, Action<IDomainTypeCollection> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureDomainModelBuilder(this LayerConfigurator configurator, Action<DomainModelBuilderOptions> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureDomainServicesModel(this LayerConfigurator configurator, Action<DomainServicesModel> configuration) =>
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

    public static bool Contains(this ModelCollection<TypeModelReference> typeReferences, Type type) =>
        typeReferences.Contains(TypeModelReference.IdFrom(type));

    public static bool Contains(this ModelCollection<TypeModelReference> typeReferences, TypeModel type) =>
        typeReferences.Contains(((IModel)type).Id);

    public static bool Contains(this ModelCollection<TypeModel> types, Type type) =>
        types.Contains(TypeModelReference.IdFrom(type));

    public static bool Has<T>(this ICustomAttributesModel model) where T : Attribute =>
        model.CustomAttributes.Contains<T>();

    public static bool Has(this ICustomAttributesModel model, Type type) =>
        model.CustomAttributes.Contains(type);

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

    public static void AddTypeMetadata(this ICollection<IDomainModelConvention> conventions, Attribute attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.AddTypeMetadata((context, add) => add(context.Type, attribute), when, order);

    public static void AddTypeMetadata(this ICollection<IDomainModelConvention> conventions, Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.Add(new MetadataConvention<TypeModelMetadataContext>(apply, when, order));

    public static void RemoveTypeMetadata<TAttribute>(this ICollection<IDomainModelConvention> conventions, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveMetadataFromTypeConvention<TAttribute>(when, order));

    public static void AddPropertyMetadata(this ICollection<IDomainModelConvention> conventions, Attribute attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.AddPropertyMetadata((context, add) => add(context.Property, attribute), when, order);

    public static void AddPropertyMetadata(this ICollection<IDomainModelConvention> conventions, Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.Add(new MetadataConvention<PropertyModelContext>(apply, when, order));

    public static void AddMethodMetadata(this ICollection<IDomainModelConvention> conventions, Attribute attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.AddMethodMetadata((context, add) => add(context.Method, attribute), when, order);

    public static void AddMethodMetadata(this ICollection<IDomainModelConvention> conventions, Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.Add(new MetadataConvention<MethodModelContext>(apply, when, order));

    public static void AddParameterMetadata(this ICollection<IDomainModelConvention> conventions, Attribute attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.AddParameterMetadata((context, add) => add(context.Parameter, attribute), when, order);

    public static void AddParameterMetadata(this ICollection<IDomainModelConvention> conventions, Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.Add(new MetadataConvention<ParameterModelContext>(apply, when, order));

    #endregion

    #region TypeModel

    public static bool Is<T>(this TypeModel type, bool allowAsync) =>
        type.Is<T>() || (allowAsync && type.Is<Task<T>>());

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

    public static void ShouldContain<TAttribute>(this ICustomAttributesModel model,
        Func<TAttribute, bool>? matcher = default
    ) where TAttribute : Attribute
    {
        matcher ??= _ => true;

        model.TryGetSingle<TAttribute>(out var attribute).ShouldBeTrue();
        matcher(attribute).ShouldBeTrue();
    }

    public static MethodModel TheMethod<T>(this Stubber giveMe, string name) =>
        giveMe
            .Spec.BakeContext
            .GetDomainModel().Types[typeof(T)]
            .GetMembers().Methods[name];

    #endregion

    #region Documentation

    static readonly string _xmlLeftIndent = new string(' ', 3 * 4);

    public static string? GetSummary(this XmlNode? xmlNode) =>
        xmlNode.GetChildTagInnerText("summary");

    public static string? GetRemarks(this XmlNode? xmlNode) =>
        xmlNode.GetChildTagInnerText("remarks");

    public static string? GetReturns(this XmlNode? xmlNode) =>
        xmlNode.GetChildTagInnerText("returns");

    public static string? GetExampleCode(this XmlNode? xmlNode, string @for,
        string exampleFor = "rest-api"
    ) => xmlNode?.SelectSingleNode($"example[@for='{exampleFor}']/code[@for='{@for}']")?.InnerText.Trim().Replace(_xmlLeftIndent, string.Empty);

    public static string? GetChildTagInnerText(this XmlNode? xmlNode, string tagName) =>
        xmlNode?[tagName]?.InnerText.Trim();

    public static string? EscapeNewLines(this string? str) =>
        str?
          .Replace(_xmlLeftIndent, string.Empty)
          .Replace("\n ", "\\n")
          .Replace("\n", "\\n")
          .Replace("\r", "\\r")
        ;

    public static XmlNode? TheDocumentation<T>(this Stubber giveMe,
        string? property = default,
        string? method = default,
        string? parameter = default
    )
    {
        var domainModel = giveMe.Spec.BakeContext.GetDomainModel();
        var type = domainModel.Types[typeof(T)];
        if (!type.TryGetMembers(out var members)) { return null; }

        if (property is not null)
        {
            return members.Properties[property].Documentation;
        }

        if (method is not null)
        {
            if (parameter is not null)
            {
                return members.Methods[method].DefaultOverload.Parameters[parameter].Documentation;
            }

            return members.Methods[method].Documentation;
        }

        return members.Documentation;
    }
    #endregion
}