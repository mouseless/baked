using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Conventions;
using Baked.Domain.Metadata;
using Baked.Domain.Model;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml;

namespace Baked;

public static class DomainExtensions
{
    public class Configurator(LayerConfigurator _configurator)
    {
        public void ConfigureDomainTypeCollection(Action<IDomainTypeCollection> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureDomainModelBuilder(Action<DomainModelBuilderOptions> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureDomainServiceCollection(Action<DomainServiceCollection> configuration) =>
            ConfigureDomainServiceCollection((services, _) => configuration(services));

        public void ConfigureDomainServiceCollection(Action<DomainServiceCollection, DomainModel> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureMetadataSetConfigurationCollection(Action<MetadataSetConfigurationCollection> configuration) =>
            _configurator.Configure(configuration);

        public void UsingDomainModel(Action<DomainModel> configuration) =>
            _configurator.Use(configuration);
    }

    extension(LayerConfigurator configurator)
    {
        public Configurator Domain => new(configurator);
    }

    extension(ICollection<ILayer> layers)
    {
        public void AddDomain() =>
            layers.Add(new DomainLayer());
    }

    extension(ApplicationContext application)
    {
        public IDomainTypeCollection GetDomainTypes() =>
            application.Get<IDomainTypeCollection>();

        public DomainModel GetDomainModel() =>
            application.Get<DomainModel>();
    }

    extension(List<DomainServiceDescriptor> serviceModels)
    {
        public void Add(TypeModel type, ServiceLifetime serviceLifetime, IEnumerable<TypeModelReference> interfaces,
            bool useFactory = true,
            bool forward = false
        ) => serviceModels.Add(new(
                ServiceType: type,
                Lifetime: serviceLifetime,
                UseFactory: useFactory,
                Interfaces: interfaces,
                Forward: forward
            ));
    }

    extension(ICollection<Type> types)
    {
        public void Add<T>() =>
            types.Add(typeof(T));
    }

    extension(Type type)
    {
        public string GetCSharpFriendlyFullName() =>
            type.IsArray ? $"{type.GetElementType()?.GetCSharpFriendlyFullName()}[]" :
            !type.IsGenericType ? type.FullName ?? type.Name :
            type.GetGenericTypeDefinition() == typeof(Nullable<>) ? $"{type.GenericTypeArguments.First().GetCSharpFriendlyFullName()}?" :
            type.Name.IndexOf("`") < 0 ? type.FullName ?? type.Name :
            $"{type.Namespace}.{type.Name[..type.Name.IndexOf("`")]}<{string.Join(", ", type.GenericTypeArguments.Select(GetCSharpFriendlyFullName))}>";

        public bool AllowsMultiple() =>
            type.IsAssignableTo(typeof(Attribute)) &&
            type.GetCustomAttributes(typeof(AttributeUsageAttribute), false)
                .Cast<AttributeUsageAttribute>()
                .FirstOrDefault()
                ?.AllowMultiple == true;
    }

    extension(ICollection<TypeBuildLevelFilter> filters)
    {
        public void Add(TypeModel.Factory buildLevel) =>
            filters.Add((Type _) => true, buildLevel);

        public void Add(Func<Type, bool> filter, TypeModel.Factory buildLevel) =>
            filters.Add(context => filter(context.Type), buildLevel);

        public void Add(Func<TypeModelBuildContext, bool> filter, TypeModel.Factory buildLevel) =>
            filters.Add(new(filter, buildLevel));
    }

    extension(IEnumerable<TypeModelReference> references)
    {
        public void Apply(Action<Type> action)
        {
            foreach (var reference in references)
            {
                reference.Apply(action);
            }
        }
    }

    extension(ModelCollection<TypeModelReference> typeReferences)
    {
        public bool Contains(Type type) =>
            typeReferences.Contains(TypeModelReference.IdFrom(type));

        public bool Contains(TypeModel type) =>
            typeReferences.Contains(((IModel)type).Id);
    }

    extension(ModelCollection<TypeModel> types)
    {
        public bool Contains(Type type) =>
            types.Contains(TypeModelReference.IdFrom(type));
    }

    extension(ICustomAttributesModel model)
    {
        public bool Has<T>() where T : Attribute =>
            model.CustomAttributes.Contains<T>();

        public bool Has(Type type) =>
            model.CustomAttributes.Contains(type);

        public T Get<T>() where T : Attribute =>
            model.CustomAttributes.Get<T>();

        public Attribute Get(Type attributeType) =>
            model.CustomAttributes.Get(attributeType);

        public bool TryGet<T>([NotNullWhen(true)] out T? result) where T : Attribute =>
            model.CustomAttributes.TryGet(out result);

        public bool TryGet(Type attributeType, [NotNullWhen(true)] out Attribute? result) =>
            model.CustomAttributes.TryGet(attributeType, out result);

        public IEnumerable<T> GetAll<T>() where T : Attribute =>
            model.CustomAttributes.GetAll<T>();

        public IEnumerable<Attribute> GetAll(Type attributeType) =>
            model.CustomAttributes.GetAll(attributeType);

        public bool TryGetAll<T>([NotNullWhen(true)] out IEnumerable<T>? result) where T : Attribute =>
            model.CustomAttributes.TryGetAll(out result);

        public bool TryGetAll(Type type, [NotNullWhen(true)] out IEnumerable<Attribute>? result) =>
            model.CustomAttributes.TryGetAll(type, out result);

        public void ShouldContain<TAttribute>(Func<TAttribute, bool>? matcher = default) where TAttribute : Attribute
        {
            matcher ??= _ => true;

            model.TryGet<TAttribute>(out var attribute).ShouldBeTrue();
            matcher(attribute).ShouldBeTrue();
        }
    }

    extension(Attribute attribute)
    {
        public bool AllowsMultiple() =>
            attribute
                .GetType()
                .AllowsMultiple();
    }

    extension(MemberInfo? memberInfo)
    {
        /// <summary>
        /// Checks if `EditorBrowsableAttribute` was added with `Advanced` state,
        /// and returns false if any.
        ///
        /// `Publicize.Fody` weaver adds this to all originally non-public members.
        /// </summary>
        public bool IsOriginallyPublic()
        {
            if (memberInfo is null) { return false; }
            if (memberInfo is PropertyInfo property) { return property.GetMethod?.IsOriginallyPublic() == true; }

            return !memberInfo.GetCustomAttributes().OfType<EditorBrowsableAttribute>().Any(eba => eba.State == EditorBrowsableState.Advanced);
        }
    }

    extension(Stubber giveMe)
    {
        public DomainModel TheDomainModel() =>
            giveMe.Spec.GenerateContext.GetDomainModel();

        public TypeModel TheTypeModel<T>() =>
            giveMe.TheTypeModel(typeof(T));

        public TypeModel TheTypeModel(Type type) =>
            giveMe.TheDomainModel().Types[type];

        public MethodModel TheMethod<T>(string name) =>
            giveMe
                .Spec.GenerateContext
                .GetDomainModel().Types[typeof(T)]
                .GetMembers().Methods[name];

        public XmlNode? TheDocumentation<T>(
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
    }

    extension(IDomainModelConventionCollection conventions)
    {
        public void Add(IDomainModelConvention convention,
            int order = default
        ) => conventions.Add((convention, order));

        public void SetTypeAttribute(Func<Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetTypeAttribute((context, set) => set(context.Type, attribute()), when, requiresIndex, order);

        public void SetTypeAttribute(Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetTypeAttribute((context, set) => set(context.Type, attribute(context)), when, requiresIndex, order);

        public void SetTypeAttribute(Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new SetAttributeConvention<TypeModelMetadataContext>(apply, when, attributeRequiredIndex: requiresIndex), order);

        public void AddTypeAttribute(Func<Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddTypeAttribute((context, add) => add(context.Type, attribute()), when, requiresIndex, order);

        public void AddTypeAttribute(Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddTypeAttribute((context, add) => add(context.Type, attribute(context)), when, requiresIndex, order);

        public void AddTypeAttribute(Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new AddAttributeConvention<TypeModelMetadataContext>(apply, when, attributeRequiresIndex: requiresIndex), order);

        public void RemoveTypeAttribute<TAttribute>(Func<TypeModelMetadataContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<TypeModelMetadataContext, TAttribute>((context, remove) => remove(context.Type), when, attributeRequiresIndex: requiresIndex), order);

        public void SetPropertyAttribute(Func<Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetPropertyAttribute((context, set) => set(context.Property, attribute()), when, requiresIndex, order);

        public void SetPropertyAttribute(Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetPropertyAttribute((context, set) => set(context.Property, attribute(context)), when, requiresIndex, order);

        public void SetPropertyAttribute(Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new SetAttributeConvention<PropertyModelContext>(apply, when, attributeRequiredIndex: requiresIndex), order);

        public void AddPropertyAttribute(Func<Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddPropertyAttribute((context, add) => add(context.Property, attribute()), when, requiresIndex, order);

        public void AddPropertyAttribute(Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddPropertyAttribute((context, add) => add(context.Property, attribute(context)), when, requiresIndex, order);

        public void AddPropertyAttribute(Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new AddAttributeConvention<PropertyModelContext>(apply, when, attributeRequiresIndex: requiresIndex), order);

        public void RemovePropertyAttribute<TAttribute>(Func<PropertyModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<PropertyModelContext, TAttribute>((context, remove) => remove(context.Property), when, attributeRequiresIndex: requiresIndex), order);

        public void SetMethodAttribute(Func<Attribute> attribute, Func<MethodModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetMethodAttribute((context, set) => set(context.Method, attribute()), when, requiresIndex, order);

        public void SetMethodAttribute(Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetMethodAttribute((context, set) => set(context.Method, attribute(context)), when, requiresIndex, order);

        public void SetMethodAttribute(Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new SetAttributeConvention<MethodModelContext>(apply, when, attributeRequiredIndex: requiresIndex), order);

        public void AddMethodAttribute(Func<Attribute> attribute, Func<MethodModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddMethodAttribute((context, add) => add(context.Method, attribute()), when, requiresIndex, order);

        public void AddMethodAttribute(Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddMethodAttribute((context, add) => add(context.Method, attribute(context)), when, requiresIndex, order);

        public void AddMethodAttribute(Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new AddAttributeConvention<MethodModelContext>(apply, when, attributeRequiresIndex: requiresIndex), order);

        public void RemoveMethodAttribute<TAttribute>(Func<MethodModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<MethodModelContext, TAttribute>((context, remove) => remove(context.Method), when, attributeRequiresIndex: requiresIndex), order);

        public void SetParameterAttribute(Func<Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetParameterAttribute((context, set) => set(context.Parameter, attribute()), when, requiresIndex, order);

        public void SetParameterAttribute(Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.SetParameterAttribute((context, set) => set(context.Parameter, attribute(context)), when, requiresIndex, order);

        public void SetParameterAttribute(Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new SetAttributeConvention<ParameterModelContext>(apply, when, attributeRequiredIndex: requiresIndex), order);

        public void AddParameterAttribute(Func<Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddParameterAttribute((context, add) => add(context.Parameter, attribute()), when, requiresIndex, order);

        public void AddParameterAttribute(Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.AddParameterAttribute((context, add) => add(context.Parameter, attribute(context)), when, requiresIndex, order);

        public void AddParameterAttribute(Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) => conventions.Add(new AddAttributeConvention<ParameterModelContext>(apply, when, attributeRequiresIndex: requiresIndex), order);

        public void RemoveParameterAttribute<TAttribute>(Func<ParameterModelContext, bool> when,
            bool requiresIndex = true,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<ParameterModelContext, TAttribute>((context, remove) => remove(context.Parameter), when, attributeRequiresIndex: requiresIndex), order);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<TypeModelMetadataContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddTypeAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<TypeModelMetadataContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddTypeAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute, TypeModelMetadataContext> attribute,
            Func<TypeModelMetadataContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddTypeAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute, TypeModelMetadataContext> attribute,
            Func<TypeModelMetadataContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new TypeAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<PropertyModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddPropertyAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<PropertyModelContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddPropertyAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute, PropertyModelContext> attribute,
            Func<PropertyModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddPropertyAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute, PropertyModelContext> attribute,
            Func<PropertyModelContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new PropertyAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<MethodModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddMethodAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<MethodModelContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddMethodAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute, MethodModelContext> attribute,
            Func<MethodModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddMethodAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute, MethodModelContext> attribute,
            Func<MethodModelContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new MethodAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<ParameterModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddParameterAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<ParameterModelContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddParameterAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute, ParameterModelContext> attribute,
            Func<ParameterModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            int order = default
        ) where TAttribute : Attribute =>
            conventions.AddParameterAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute, ParameterModelContext> attribute,
            Func<ParameterModelContext, TAttribute, bool>? when = default,
            int order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new ParameterAttributeConfigurationConvention<TAttribute>(attribute, when: when), order: order);
    }

    extension(TypeModel type)
    {
        public bool Is<T>(bool allowAsync) =>
            type.Is<T>() || (allowAsync && type.Is<Task<T>>());

        public bool HasGenerics() =>
            type.HasInfo<TypeModelGenerics>();

        public TypeModelGenerics GetGenerics() =>
            type.GetInfo<TypeModelGenerics>();

        public bool TryGetGenerics([NotNullWhen(true)] out TypeModelGenerics? result) =>
            type.TryGetInfo(out result);

        public bool HasInheritance() =>
            type.HasInfo<TypeModelInheritance>();

        public TypeModelInheritance GetInheritance() =>
            type.GetInfo<TypeModelInheritance>();

        public bool TryGetInheritance([NotNullWhen(true)] out TypeModelInheritance? result) =>
            type.TryGetInfo(out result);

        public bool HasMetadata() =>
            type.HasInfo<TypeModelMetadata>();

        public TypeModelMetadata GetMetadata() =>
            type.GetInfo<TypeModelMetadata>();

        public bool TryGetMetadata([NotNullWhen(true)] out TypeModelMetadata? result) =>
            type.TryGetInfo(out result);

        public bool HasMembers() =>
            type.HasInfo<TypeModelMembers>();

        public TypeModelMembers GetMembers() =>
            type.GetInfo<TypeModelMembers>();

        public bool TryGetMembers([NotNullWhen(true)] out TypeModelMembers? result) =>
            type.TryGetInfo(out result);

#pragma warning disable IDE0051
        bool HasInfo<TInfo>() where TInfo : TypeModel =>
            type is TInfo;

        TInfo GetInfo<TInfo>() where TInfo : TypeModel =>
            (TInfo)type;

        bool TryGetInfo<TInfo>([NotNullWhen(true)] out TInfo? result)
            where TInfo : TypeModel
        {
            result = type as TInfo;

            return result is not null;
        }
#pragma warning restore IDE0051
    }

#pragma warning disable IDE0051
    static Dictionary<Type, AttributeUsageAttribute?> _usageCache = new();

    static AttributeUsageAttribute? GetAttributeUsage<T>()
    {
        if (!_usageCache.TryGetValue(typeof(T), out var usage))
        {
            usage = (AttributeUsageAttribute?)Attribute.GetCustomAttribute(typeof(T), typeof(AttributeUsageAttribute));
            _usageCache[typeof(T)] = usage;
        }

        return usage;
    }
#pragma warning restore IDE0051

    extension(MetadataModelBuilderOptions options)
    {
        public void AddAttribute<T>() where T : Attribute
        {
            var usage = GetAttributeUsage<T>();
            if (usage is null) { return; }

            if (usage.ValidOn == AttributeTargets.Class)
            {
                options.TypeAttributes.Add(typeof(T));
            }

            if (usage.ValidOn == AttributeTargets.Method)
            {
                options.MethodAttributes.Add(typeof(T));
            }

            if (usage.ValidOn == AttributeTargets.Parameter)
            {
                options.ParameterAttributes.Add(typeof(T));
            }

            if (usage.ValidOn == AttributeTargets.Property)
            {
                options.PropertyAttributes.Add(typeof(T));
            }
        }

        public void RemoveAttribute<T>() where T : Attribute
        {
            var usage = GetAttributeUsage<T>();
            if (usage is null) { return; }

            if (usage.ValidOn == AttributeTargets.Class)
            {
                options.TypeAttributes.Remove(typeof(T));
            }

            if (usage.ValidOn == AttributeTargets.Method)
            {
                options.MethodAttributes.Remove(typeof(T));
            }

            if (usage.ValidOn == AttributeTargets.Parameter)
            {
                options.ParameterAttributes.Remove(typeof(T));
            }

            if (usage.ValidOn == AttributeTargets.Property)
            {
                options.PropertyAttributes.Remove(typeof(T));
            }
        }
    }

#pragma warning disable IDE0052
    static readonly string _xmlLeftIndent = new string(' ', 3 * 4);
#pragma warning restore IDE0052

    extension(XmlNode? xmlNode)
    {
        public string? Summary =>
            xmlNode.GetChildTagInnerText("summary");

        public string? Remarks =>
            xmlNode.GetChildTagInnerText("remarks");

        public string? Returns =>
            xmlNode.GetChildTagInnerText("returns");

        public string? GetExampleCode(string @for,
            string exampleFor = "rest-api"
        ) => xmlNode?.SelectSingleNode($"example[@for='{exampleFor}']/code[@for='{@for}']")?.InnerText.Trim().Replace(_xmlLeftIndent, string.Empty);

        public string? GetChildTagInnerText(string tagName) =>
            xmlNode?[tagName]?.InnerText.Trim();
    }

    extension(string? str)
    {
        public string? EscapeNewLines() =>
            str?
            .Replace(_xmlLeftIndent, string.Empty)
            .Replace("\n ", "\\n")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            ;
    }
}