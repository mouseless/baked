using Baked.Architecture;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Export;
using Baked.Domain.Inspection;
using Baked.Domain.Model;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Baked;

public static class DomainExtensions
{
    public class Configurator(LayerConfigurator _configurator)
    {
        public void ConfigureInspect(Action<Inspect> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureDomainTypeCollection(Action<IDomainTypeCollection> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureBuilder(Action<DomainModelBuilderOptions> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureConventions(Action<IDomainModelConventionCollection> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureDomainServiceCollection(Action<DomainServiceCollection> configuration) =>
            ConfigureDomainServiceCollection((services, _) => configuration(services));

        public void ConfigureDomainServiceCollection(Action<DomainServiceCollection, DomainModel> configuration) =>
            _configurator.Configure(configuration);

        public void ConfigureAttributeProperties(Action<AttributeProperties> configuration) =>
           _configurator.Configure(configuration);

        public void ConfigureExportConfigurations(Action<ExportConfigurations> configuration) =>
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

    extension(DiagnosticCode)
    {
        public static DiagnosticCode AttributeTargetMismatch => new(301, "attribute-target-mismatch");
        public static DiagnosticCode AttributeDoesNotAllow => new(302, "attribute-does-not-allow");
        public static DiagnosticCode OrderOutOfBounds => new(303, "order-out-of-bounds");
        public static DiagnosticCode UndefinedLevel => new(304, "undefined-level");
        public static DiagnosticCode InvalidOrder => new(305, "invalid-order");
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

        public bool IsAnonymous =>
            type.Namespace == null &&
            type.IsSealed &&
            type.IsGenericType &&
            type.Name.Contains("AnonymousType") &&
            Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute));
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

        public void ThrowIfNotTarget(ICustomAttributesModel model)
        {
            var usages = (AttributeUsageAttribute?)Attribute.GetCustomAttribute(attribute.GetType(), typeof(AttributeUsageAttribute));
            var validOn = usages?.ValidOn ?? AttributeTargets.All;
            if (validOn.HasFlag(model.Target)) { return; }

            throw DiagnosticCode.AttributeTargetMismatch.Exception(
                $"'{attribute.GetType().Name}' does not have '{model.Target}' target. Available targets: '{validOn}'"
            );
        }
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

    extension(PropertyInfo propertyInfo)
    {
        public bool IsAutoProperty
        {
            get
            {
                var getMethod = propertyInfo.GetGetMethod();
                if (getMethod is null) { return false; }

                return getMethod.GetCustomAttribute<CompilerGeneratedAttribute>() is not null;
            }
        }
    }

    extension(Order order)
    {
        public Order Defaults =>
            order.WithLevel("Defaults");

        public Order Custom =>
            order.WithLevel("Custom");

        public Order Override =>
            order.WithLevel("Override");

        public Order Add =>
            order.WithExtension("Add");

        public Order Configure =>
            order.WithExtension("Configure");
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

        // WARNING
        //
        // Do NOT remove this warning disable section unintentionally.
        // Without this, GitHub Actions fails on dotnet format
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

    extension<TModelContext>(Func<TModelContext, bool>? when)
        where TModelContext : DomainModelContext
    {
        public Func<DomainModelContext, bool>? GeneralizeOrDefault() =>
            when is not null
                ? c => c is TModelContext mc && when(mc)
                : null;
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

    extension(Stubber giveMe)
    {
        public DomainModel TheDomainModel() =>
            giveMe.Spec.GenerateContext.GetDomainModel();

        public TypeModel TheTypeModel<T>() =>
            giveMe.TheTypeModel(typeof(T));

        public TypeModel TheTypeModel(Type type) =>
            giveMe.TheDomainModel().Types[type];

        public MethodModel TheMethodModel<T>(string name) =>
            giveMe
                .Spec.GenerateContext
                .GetDomainModel().Types[typeof(T)]
                .GetMembers().Methods[name];

        public TypeModelContext ATypeModelContext<T>() =>
            new()
            {
                Domain = giveMe.TheDomainModel(),
                Type = giveMe.TheTypeModel<T>()
            };

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
}