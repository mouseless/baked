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
        type.Name.IndexOf("`") < 0 ? type.FullName ?? type.Name :
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

    public static DomainModel TheDomainModel(this Stubber giveMe) =>
        giveMe.Spec.GenerateContext.GetDomainModel();

    public static TypeModel TheTypeModel<T>(this Stubber giveMe) =>
        giveMe.TheTypeModel(typeof(T));

    public static TypeModel TheTypeModel(this Stubber giveMe, Type type) =>
        giveMe.TheDomainModel().Types[type];

    #region IDomainModelConvention

    public static void Add(this IDomainModelConventionCollection conventions, IDomainModelConvention convention,
        int order = default
    ) => conventions.Add((convention, order));

    #region Attribute Add/Set/Remove

    public static void SetTypeAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.SetTypeAttribute((context, set) => set(context.Type, attribute()), when, order);

    public static void SetTypeAttribute(this IDomainModelConventionCollection conventions, Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.SetTypeAttribute((context, set) => set(context.Type, attribute(context)), when, order);

    public static void SetTypeAttribute(this IDomainModelConventionCollection conventions, Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.Add(new SetAttributeConvention<TypeModelMetadataContext>(apply, when), order);

    public static void AddTypeAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.AddTypeAttribute((context, add) => add(context.Type, attribute()), when, order);

    public static void AddTypeAttribute(this IDomainModelConventionCollection conventions, Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.AddTypeAttribute((context, add) => add(context.Type, attribute(context)), when, order);

    public static void AddTypeAttribute(this IDomainModelConventionCollection conventions, Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) => conventions.Add(new AddAttributeConvention<TypeModelMetadataContext>(apply, when), order);

    public static void RemoveTypeAttribute<TAttribute>(this IDomainModelConventionCollection conventions, Func<TypeModelMetadataContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveAttributeConvention<TypeModelMetadataContext, TAttribute>((context, remove) => remove(context.Type), when), order);

    public static void SetPropertyAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.SetPropertyAttribute((context, set) => set(context.Property, attribute()), when, order);

    public static void SetPropertyAttribute(this IDomainModelConventionCollection conventions, Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.SetPropertyAttribute((context, set) => set(context.Property, attribute(context)), when, order);

    public static void SetPropertyAttribute(this IDomainModelConventionCollection conventions, Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.Add(new SetAttributeConvention<PropertyModelContext>(apply, when), order);

    public static void AddPropertyAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.AddPropertyAttribute((context, add) => add(context.Property, attribute()), when, order);

    public static void AddPropertyAttribute(this IDomainModelConventionCollection conventions, Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.AddPropertyAttribute((context, add) => add(context.Property, attribute(context)), when, order);

    public static void AddPropertyAttribute(this IDomainModelConventionCollection conventions, Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
        int order = default
    ) => conventions.Add(new AddAttributeConvention<PropertyModelContext>(apply, when), order);

    public static void RemovePropertyAttribute<TAttribute>(this IDomainModelConventionCollection conventions, Func<PropertyModelContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveAttributeConvention<PropertyModelContext, TAttribute>((context, remove) => remove(context.Property), when), order);

    public static void SetMethodAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.SetMethodAttribute((context, set) => set(context.Method, attribute()), when, order);

    public static void SetMethodAttribute(this IDomainModelConventionCollection conventions, Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.SetMethodAttribute((context, set) => set(context.Method, attribute(context)), when, order);

    public static void SetMethodAttribute(this IDomainModelConventionCollection conventions, Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.Add(new SetAttributeConvention<MethodModelContext>(apply, when), order);

    public static void AddMethodAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.AddMethodAttribute((context, add) => add(context.Method, attribute()), when, order);

    public static void AddMethodAttribute(this IDomainModelConventionCollection conventions, Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.AddMethodAttribute((context, add) => add(context.Method, attribute(context)), when, order);

    public static void AddMethodAttribute(this IDomainModelConventionCollection conventions, Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
        int order = default
    ) => conventions.Add(new AddAttributeConvention<MethodModelContext>(apply, when), order);

    public static void RemoveMethodAttribute<TAttribute>(this IDomainModelConventionCollection conventions, Func<MethodModelContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveAttributeConvention<MethodModelContext, TAttribute>((context, remove) => remove(context.Method), when), order);

    public static void SetParameterAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.SetParameterAttribute((context, set) => set(context.Parameter, attribute()), when, order);

    public static void SetParameterAttribute(this IDomainModelConventionCollection conventions, Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.SetParameterAttribute((context, set) => set(context.Parameter, attribute(context)), when, order);

    public static void SetParameterAttribute(this IDomainModelConventionCollection conventions, Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.Add(new SetAttributeConvention<ParameterModelContext>(apply, when), order);

    public static void AddParameterAttribute(this IDomainModelConventionCollection conventions, Func<Attribute> attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.AddParameterAttribute((context, add) => add(context.Parameter, attribute()), when, order);

    public static void AddParameterAttribute(this IDomainModelConventionCollection conventions, Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.AddParameterAttribute((context, add) => add(context.Parameter, attribute(context)), when, order);

    public static void AddParameterAttribute(this IDomainModelConventionCollection conventions, Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
        int order = default
    ) => conventions.Add(new AddAttributeConvention<ParameterModelContext>(apply, when), order);

    public static void RemoveParameterAttribute<TAttribute>(this IDomainModelConventionCollection conventions, Func<ParameterModelContext, bool> when,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new RemoveAttributeConvention<ParameterModelContext, TAttribute>((context, remove) => remove(context.Parameter), when), order);

    #endregion

    #region Attribute Configuration

    public static void AddTypeAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<TypeModelMetadataContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddTypeAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

    public static void AddTypeAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<TypeModelMetadataContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddTypeAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

    public static void AddTypeAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, TypeModelMetadataContext> attribute,
        Func<TypeModelMetadataContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddTypeAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

    public static void AddTypeAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, TypeModelMetadataContext> attribute,
        Func<TypeModelMetadataContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new TypeAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);

    public static void AddPropertyAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<PropertyModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddPropertyAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

    public static void AddPropertyAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<PropertyModelContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddPropertyAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

    public static void AddPropertyAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, PropertyModelContext> attribute,
        Func<PropertyModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddPropertyAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

    public static void AddPropertyAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, PropertyModelContext> attribute,
        Func<PropertyModelContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new PropertyAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);

    public static void AddMethodAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<MethodModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddMethodAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

    public static void AddMethodAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<MethodModelContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddMethodAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

    public static void AddMethodAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, MethodModelContext> attribute,
        Func<MethodModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddMethodAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

    public static void AddMethodAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, MethodModelContext> attribute,
        Func<MethodModelContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new MethodAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);

    public static void AddParameterAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<ParameterModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddParameterAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

    public static void AddParameterAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute> attribute,
        Func<ParameterModelContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddParameterAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

    public static void AddParameterAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, ParameterModelContext> attribute,
        Func<ParameterModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
        int order = default
    ) where TAttribute : Attribute =>
        conventions.AddParameterAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

    public static void AddParameterAttributeConfiguration<TAttribute>(this IDomainModelConventionCollection conventions, Action<TAttribute, ParameterModelContext> attribute,
        Func<ParameterModelContext, TAttribute, bool>? when = default,
        int order = default
    ) where TAttribute : Attribute =>
        conventions.Add(new ParameterAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);

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