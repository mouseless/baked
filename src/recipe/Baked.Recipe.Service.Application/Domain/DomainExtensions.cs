using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Conventions;
using Baked.Domain.Model;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
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

    public static void ConfigureDomainServiceCollection(this LayerConfigurator configurator, Action<DomainServiceCollection> configuration) =>
        configurator.ConfigureDomainServiceCollection((services, _) => configuration(services));

    public static void ConfigureDomainServiceCollection(this LayerConfigurator configurator, Action<DomainServiceCollection, DomainModel> configuration) =>
        configurator.Configure(configuration);

    public static void UsingDomainModel(this LayerConfigurator configurator, Action<DomainModel> configuration) =>
        configurator.Use(configuration);

    public static void Add(this List<DomainServiceDescriptor> serviceModels, TypeModel type, ServiceLifetime serviceLifetime, IEnumerable<TypeModelReference> interfaces,
        bool useFactory = true,
        bool forward = false
    ) => serviceModels.Add(new(
            ServiceType: type,
            Lifetime: serviceLifetime,
            UseFactory: useFactory,
            Interfaces: interfaces,
            Forward: forward
        ));

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

    public static T Get<T>(this ICustomAttributesModel model) where T : Attribute =>
        model.CustomAttributes.Get<T>();

    public static Attribute Get(this ICustomAttributesModel model, Type attributeType) =>
        model.CustomAttributes.Get(attributeType);

    public static bool TryGet<T>(this ICustomAttributesModel model, [NotNullWhen(true)] out T? result) where T : Attribute =>
        model.CustomAttributes.TryGet(out result);

    public static bool TryGet(this ICustomAttributesModel model, Type attributeType, [NotNullWhen(true)] out Attribute? result) =>
        model.CustomAttributes.TryGet(attributeType, out result);

    public static IEnumerable<T> GetAll<T>(this ICustomAttributesModel model) where T : Attribute =>
        model.CustomAttributes.GetAll<T>();

    public static IEnumerable<Attribute> GetAll(this ICustomAttributesModel model, Type attributeType) =>
        model.CustomAttributes.GetAll(attributeType);

    public static bool TryGetAll<T>(this ICustomAttributesModel model, [NotNullWhen(true)] out IEnumerable<T>? result) where T : Attribute =>
        model.CustomAttributes.TryGetAll(out result);

    public static bool TryGetAll(this ICustomAttributesModel model, Type type, [NotNullWhen(true)] out IEnumerable<Attribute>? result) =>
        model.CustomAttributes.TryGetAll(type, out result);

    public static bool AllowsMultiple(this Attribute attribute) =>
        attribute
            .GetType()
            .AllowsMultiple();

    public static bool AllowsMultiple(this Type type) =>
        type.IsAssignableTo(typeof(Attribute)) &&
        type.GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .FirstOrDefault()
            ?.AllowMultiple == true;

    #region IDomainModelConvention

    public static void Add(this IDomainModelConventionCollection conventions, IDomainModelConvention convention,
        int order = default
    ) => conventions.Add((convention, order));

    #region Metadata

    public static void SetTypeMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.SetTypeMetadata((context, set) => set(context.Type, attribute), when, order);

    public static void SetTypeMetadata(this IDomainModelConventionCollection conventions, Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.SetTypeMetadata((context, set) => set(context.Type, attribute(context)), when, order);

    public static void SetTypeMetadata(this IDomainModelConventionCollection conventions, Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.Add(new SetMetadataConvention<TypeModelMetadataContext>(apply, when), order);

    public static void AddTypeMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.AddTypeMetadata((context, add) => add(context.Type, attribute), when, order);

    public static void AddTypeMetadata(this IDomainModelConventionCollection conventions, Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.AddTypeMetadata((context, add) => add(context.Type, attribute(context)), when, order);

    public static void AddTypeMetadata(this IDomainModelConventionCollection conventions, Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.Add(new AddMetadataConvention<TypeModelMetadataContext>(apply, when), order);

    public static void RemoveTypeMetadata<TAttribute>(this IDomainModelConventionCollection conventions, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveMetadataConvention<TypeModelMetadataContext, TAttribute>((context, remove) => remove(context.Type), when), order);

    public static void SetPropertyMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.SetPropertyMetadata((context, set) => set(context.Property, attribute), when, order);

    public static void SetPropertyMetadata(this IDomainModelConventionCollection conventions, Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.SetPropertyMetadata((context, set) => set(context.Property, attribute(context)), when, order);

    public static void SetPropertyMetadata(this IDomainModelConventionCollection conventions, Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.Add(new SetMetadataConvention<PropertyModelContext>(apply, when), order);

    public static void AddPropertyMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.AddPropertyMetadata((context, add) => add(context.Property, attribute), when, order);

    public static void AddPropertyMetadata(this IDomainModelConventionCollection conventions, Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.AddPropertyMetadata((context, add) => add(context.Property, attribute(context)), when, order);

    public static void AddPropertyMetadata(this IDomainModelConventionCollection conventions, Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.Add(new AddMetadataConvention<PropertyModelContext>(apply, when), order);

    public static void RemovePropertyMetadata<TAttribute>(this IDomainModelConventionCollection conventions, Func<PropertyModelContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveMetadataConvention<PropertyModelContext, TAttribute>((context, remove) => remove(context.Property), when), order);

    public static void SetMethodMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.SetMethodMetadata((context, set) => set(context.Method, attribute), when, order);

    public static void SetMethodMetadata(this IDomainModelConventionCollection conventions, Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.SetMethodMetadata((context, set) => set(context.Method, attribute(context)), when, order);

    public static void SetMethodMetadata(this IDomainModelConventionCollection conventions, Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.Add(new SetMetadataConvention<MethodModelContext>(apply, when), order);

    public static void AddMethodMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.AddMethodMetadata((context, add) => add(context.Method, attribute), when, order);

    public static void AddMethodMetadata(this IDomainModelConventionCollection conventions, Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.AddMethodMetadata((context, add) => add(context.Method, attribute(context)), when, order);

    public static void AddMethodMetadata(this IDomainModelConventionCollection conventions, Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.Add(new AddMetadataConvention<MethodModelContext>(apply, when), order);

    public static void RemoveMethodMetadata<TAttribute>(this IDomainModelConventionCollection conventions, Func<MethodModelContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveMetadataConvention<MethodModelContext, TAttribute>((context, remove) => remove(context.Method), when), order);

    public static void SetParameterMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.SetParameterMetadata((context, set) => set(context.Parameter, attribute), when, order);

    public static void SetParameterMetadata(this IDomainModelConventionCollection conventions, Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.SetParameterMetadata((context, set) => set(context.Parameter, attribute(context)), when, order);

    public static void SetParameterMetadata(this IDomainModelConventionCollection conventions, Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.Add(new SetMetadataConvention<ParameterModelContext>(apply, when), order);

    public static void AddParameterMetadata(this IDomainModelConventionCollection conventions, Attribute attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.AddParameterMetadata((context, add) => add(context.Parameter, attribute), when, order);

    public static void AddParameterMetadata(this IDomainModelConventionCollection conventions, Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.AddParameterMetadata((context, add) => add(context.Parameter, attribute(context)), when, order);

    public static void AddParameterMetadata(this IDomainModelConventionCollection conventions, Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.Add(new AddMetadataConvention<ParameterModelContext>(apply, when), order);

    public static void RemoveParameterMetadata<TAttribute>(this IDomainModelConventionCollection conventions, Func<ParameterModelContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveMetadataConvention<ParameterModelContext, TAttribute>((context, remove) => remove(context.Parameter), when), order);

    #endregion

    #region Convention

    public static void AddTypeConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddTypeConvention((a, _) => apply(a), when: when, order: order);

    public static void AddTypeConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, TypeModelMetadataContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddTypeConvention((a, _) => apply(a), when: when, order: order);

    public static void AddTypeConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, TypeModelMetadataContext> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddTypeConvention(apply, when: (a, _) => when(a), order: order);

    public static void AddTypeConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, TypeModelMetadataContext> apply,
        Func<TAttribute, TypeModelMetadataContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new TypeConvention<TAttribute>(apply, when: when), order: order);

    public static void AddPropertyConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddPropertyConvention((a, _) => apply(a), when: when, order: order);

    public static void AddPropertyConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, PropertyModelContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddPropertyConvention((a, _) => apply(a), when: when, order: order);

    public static void AddPropertyConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, PropertyModelContext> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddPropertyConvention(apply, when: (a, _) => when(a), order: order);

    public static void AddPropertyConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, PropertyModelContext> apply,
        Func<TAttribute, PropertyModelContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new PropertyConvention<TAttribute>(apply, when: when), order: order);

    public static void AddMethodConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddMethodConvention((a, _) => apply(a), when: when, order: order);

    public static void AddMethodConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, MethodModelContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddMethodConvention((a, _) => apply(a), when: when, order: order);

    public static void AddMethodConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, MethodModelContext> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddMethodConvention(apply, when: (a, _) => when(a), order: order);

    public static void AddMethodConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, MethodModelContext> apply,
        Func<TAttribute, MethodModelContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new MethodConvention<TAttribute>(apply, when: when), order: order);

    public static void AddParameterConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddParameterConvention((a, _) => apply(a), when: when, order: order);

    public static void AddParameterConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> apply,
        Func<TAttribute, ParameterModelContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddParameterConvention((a, _) => apply(a), when: when, order: order);

    public static void AddParameterConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, ParameterModelContext> apply,
        Func<TAttribute, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddParameterConvention(apply, when: (a, _) => when(a), order: order);

    public static void AddParameterConvention<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, ParameterModelContext> apply,
        Func<TAttribute, ParameterModelContext, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new ParameterConvention<TAttribute>(apply, when: when), order: order);

    #endregion

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

        model.TryGet<TAttribute>(out var attribute).ShouldBeTrue();
        matcher(attribute).ShouldBeTrue();
    }

    public static MethodModel TheMethod<T>(this Stubber giveMe, string name) =>
        giveMe
            .Spec.GenerateContext
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
        var domainModel = giveMe.Spec.GenerateContext.GetDomainModel();
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