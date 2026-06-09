using Baked.Architecture;
using Baked.Business;
using Baked.Domain;
using Baked.Domain.Configuration;
using Baked.Domain.Conventions;
using Baked.Domain.Model;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Baked;

public static class BusinessExtensions
{
    extension(List<IFeature> features)
    {
        public void AddBusiness(FeatureFunc<BusinessConfigurator> configure) =>
            features.Add(configure(new()));
    }

    extension(DiagnosticCode)
    {
        public static DiagnosticCode TypeWithAttribute => new(001, "type-with-attribute");
        public static DiagnosticCode RequiresBuildLevel => new(002, "requires-build-level");
        public static DiagnosticCode PropertyWithAttribute => new(003, "property-with-attribute");
        public static DiagnosticCode MethodWithAttribute => new(004, "method-with-attribute");
        public static DiagnosticCode ParameterWithAttribute => new(005, "parameter-with-attribute");
        public static DiagnosticCode RequiresElementType => new(006, "requires-element-type");
    }

    extension(Order order)
    {
        public Order Business =>
            order.WithBase("Business");

        internal Order BusinessDefault =>
            order.WithBase(order.Base ?? "Business");
    }

    // WARNING
    //
    // Do NOT remove this warning disable section unintentionally.
    // Without this, GitHub Actions fails on dotnet format
#pragma warning disable IDE0052
    static readonly MethodInfo _addTransientWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddTransientWithFactory), 2, [typeof(IServiceCollection)]) ??
        throw new("AddTransientWithFactory<TService, TImplementation> should have existed");

    static readonly MethodInfo _addScopedWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddScopedWithFactory), 2, [typeof(IServiceCollection)]) ??
        throw new("AddScopedWithFactory<TService, TImplementation> should have existed");
#pragma warning restore IDE0052

    extension(IServiceCollection services)
    {
        public IServiceCollection AddTransientWithFactory(Type service) =>
            (IServiceCollection?)_addTransientWithFactory.MakeGenericMethod(service, service).Invoke(null, [services]) ??
            throw new("Should've returned an IServiceCollection instance");
        public IServiceCollection AddTransientWithFactory(Type service, Type implementation) =>
            (IServiceCollection?)_addTransientWithFactory.MakeGenericMethod(service, implementation).Invoke(null, [services]) ??
            throw new("Should've returned an IServiceCollection instance");

        public IServiceCollection AddTransientWithFactory<TService>() where TService : class =>
            services.AddTransientWithFactory<TService, TService>();

        public IServiceCollection AddTransientWithFactory<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        => services
            .AddSingleton<Func<TService>>(sp => () => sp.UsingCurrentScope().GetRequiredService<TService>())
            .AddTransient<TService, TImplementation>();

        public IServiceCollection AddScopedWithFactory(Type service) =>
            (IServiceCollection?)_addScopedWithFactory.MakeGenericMethod(service, service).Invoke(null, [services]) ??
            throw new("Should've returned an IServiceCollection instance");
        public IServiceCollection AddScopedWithFactory(Type service, Type implementation) =>
            (IServiceCollection?)_addScopedWithFactory.MakeGenericMethod(service, implementation).Invoke(null, [services]) ??
            throw new("Should've returned an IServiceCollection instance");
        public void AddScopedWithFactory<TService>() where TService : class =>
            services.AddScopedWithFactory<TService, TService>();

        public IServiceCollection AddScopedWithFactory<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        => services
            .AddSingleton<Func<TService>>(sp => () => sp.UsingCurrentScope().GetRequiredService<TService>())
            .AddScoped<TService, TImplementation>();

        public IServiceCollection AddSingleton<TService, TImplementation>(bool forward)
            where TService : class
            where TImplementation : class, TService
        => services.AddSingleton(typeof(TService), typeof(TImplementation), forward: forward);

        public IServiceCollection AddSingleton(Type service, Type implementation, bool forward)
        {
            if (!forward) { return services.AddSingleton(service, implementation); }

            return services.AddSingleton(service, sp => sp.UsingCurrentScope().GetRequiredService(implementation));
        }
    }

    extension(Type type)
    {
        public Type SkipNullable() =>
            Nullable.GetUnderlyingType(type) ?? type;
    }

    extension(TypeModel type)
    {
        public bool TryGetNamespace([NotNullWhen(true)] out string? @namespace)
        {
            if (!type.TryGetNamespaceAttribute(out var namespaceAttribute) || string.IsNullOrWhiteSpace(namespaceAttribute.Value))
            {
                @namespace = null;

                return false;
            }

            @namespace = namespaceAttribute.Value;

            return true;
        }

        public bool TryGetNamespaceAttribute([NotNullWhen(true)] out NamespaceAttribute? namespaceAttribute)
        {
            namespaceAttribute = default;

            return
                type.TryGetMetadata(out var metadata) &&
                metadata.TryGet(out namespaceAttribute);
        }

        public bool HasIdInfo() =>
            type.TryGetIdInfo(out var _);

        public IdInfo GetIdInfo()
        {
            var idProperty = type.GetMembers().FirstProperty<IdAttribute>();

            return new(idProperty);
        }

        public bool TryGetIdInfo([NotNullWhen(true)] out IdInfo? idInfo)
        {
            idInfo = null;

            if (!type.TryGetMembers(out var members)) { return false; }

            var idProperty = members.FirstPropertyOrDefault<IdAttribute>();
            if (idProperty is null) { return false; }

            idInfo = new(idProperty);

            return true;
        }

        public TypeModel SkipNullable()
        {
            if (!type.IsAssignableTo(typeof(Nullable<>))) { return type; }
            if (!type.TryGetGenerics(out var generics))
            {
                throw DiagnosticCode.RequiresBuildLevel.Exception($"{type.Name} doesn't provide generics information to skip nullable");
            }

            if (type.IsGenericTypeDefinition) { return type; }

            return generics.GenericTypeArguments.First().Model;
        }

        public TypeModel SkipTask() =>
            type.IsAssignableTo<Task>() && type.IsGenericType
                ? type.GetGenerics().GenericTypeArguments.First().Model
                : type;
    }

    extension(TypeModelMembers members)
    {
        public PropertyModel FirstProperty<TAttribute>(
            Func<PropertyModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            members.FirstPropertyOrDefault<TAttribute>(filter: filter) ??
            throw DiagnosticCode.PropertyWithAttribute.Exception(
                $"{members.Name} is expected to have at least one property with `{typeof(TAttribute).Name}`"
            );

        public PropertyModel? FirstPropertyOrDefault<TAttribute>(
            Func<PropertyModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            members.Properties.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));

        public MethodModel FirstMethod<TAttribute>(
            Func<MethodModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            members.FirstMethodOrDefault<TAttribute>(filter: filter) ??
            throw DiagnosticCode.MethodWithAttribute.Exception(
                $"{members.Name} is expected to have at least one method with `{typeof(TAttribute).Name}`"
            );

        public MethodModel? FirstMethodOrDefault<TAttribute>(
            Func<MethodModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            members.Methods.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));
    }

    extension(MethodModel method)
    {
        public ParameterModel FirstParameter<TAttribute>(
            Func<ParameterModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            method.FirstParameterOrDefault<TAttribute>(filter: filter) ??
            throw DiagnosticCode.ParameterWithAttribute.Exception(
                $"{method.Name} is expected to have at least one parameter with `{typeof(TAttribute).Name}`"
            );

        public ParameterModel? FirstParameterOrDefault<TAttribute>(
            Func<ParameterModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            method.DefaultOverload.Parameters.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));
    }

    extension(ParameterModel parameter)
    {
        public bool IsNullable =>
            !parameter.Has<NotNullAttribute>();

        public void ShouldBeRequired() =>
            parameter.Has<RequiredAttribute>().ShouldBeTrue($"{parameter.Name} should have `[RequiredAttribute]`");

        public void ShouldNotBeRequired() =>
            parameter.Has<RequiredAttribute>().ShouldBeFalse($"{parameter.Name} should not have `[RequiredAttribute]`");
    }

    extension(IDomainModelConventionCollection conventions)
    {
        public void SetTypeAttribute(Func<Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetTypeAttribute((context, set) => set(context.Type, attribute()), when, beforeBuildingIndexes, order);

        public void SetTypeAttribute(Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetTypeAttribute((context, set) => set(context.Type, attribute(context)), when, beforeBuildingIndexes, order);

        public void SetTypeAttribute(Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new SetAttributeConvention<TypeModelMetadataContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void AddTypeAttribute(Func<Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddTypeAttribute((context, add) => add(context.Type, attribute()), when, beforeBuildingIndexes, order);

        public void AddTypeAttribute(Func<TypeModelMetadataContext, Attribute> attribute, Func<TypeModelMetadataContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddTypeAttribute((context, add) => add(context.Type, attribute(context)), when, beforeBuildingIndexes, order);

        public void AddTypeAttribute(Action<TypeModelMetadataContext, Action<ICustomAttributesModel, Attribute>> apply, Func<TypeModelMetadataContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new AddAttributeConvention<TypeModelMetadataContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void RemoveTypeAttribute<TAttribute>(Func<TypeModelMetadataContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<TypeModelMetadataContext, TAttribute>((context, remove) => remove(context.Type), when, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void SetPropertyAttribute(Func<Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetPropertyAttribute((context, set) => set(context.Property, attribute()), when, beforeBuildingIndexes, order);

        public void SetPropertyAttribute(Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetPropertyAttribute((context, set) => set(context.Property, attribute(context)), when, beforeBuildingIndexes, order);

        public void SetPropertyAttribute(Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new SetAttributeConvention<PropertyModelContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void AddPropertyAttribute(Func<Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddPropertyAttribute((context, add) => add(context.Property, attribute()), when, beforeBuildingIndexes, order);

        public void AddPropertyAttribute(Func<PropertyModelContext, Attribute> attribute, Func<PropertyModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddPropertyAttribute((context, add) => add(context.Property, attribute(context)), when, beforeBuildingIndexes, order);

        public void AddPropertyAttribute(Action<PropertyModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<PropertyModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new AddAttributeConvention<PropertyModelContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void RemovePropertyAttribute<TAttribute>(Func<PropertyModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<PropertyModelContext, TAttribute>((context, remove) => remove(context.Property), when, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void SetMethodAttribute(Func<Attribute> attribute, Func<MethodModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetMethodAttribute((context, set) => set(context.Method, attribute()), when, beforeBuildingIndexes, order);

        public void SetMethodAttribute(Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetMethodAttribute((context, set) => set(context.Method, attribute(context)), when, beforeBuildingIndexes, order);

        public void SetMethodAttribute(Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new SetAttributeConvention<MethodModelContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void AddMethodAttribute(Func<Attribute> attribute, Func<MethodModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddMethodAttribute((context, add) => add(context.Method, attribute()), when, beforeBuildingIndexes, order);

        public void AddMethodAttribute(Func<MethodModelContext, Attribute> attribute, Func<MethodModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddMethodAttribute((context, add) => add(context.Method, attribute(context)), when, beforeBuildingIndexes, order);

        public void AddMethodAttribute(Action<MethodModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<MethodModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new AddAttributeConvention<MethodModelContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void RemoveMethodAttribute<TAttribute>(Func<MethodModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<MethodModelContext, TAttribute>((context, remove) => remove(context.Method), when, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void SetParameterAttribute(Func<Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetParameterAttribute((context, set) => set(context.Parameter, attribute()), when, beforeBuildingIndexes, order);

        public void SetParameterAttribute(Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.SetParameterAttribute((context, set) => set(context.Parameter, attribute(context)), when, beforeBuildingIndexes, order);

        public void SetParameterAttribute(Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new SetAttributeConvention<ParameterModelContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void AddParameterAttribute(Func<Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddParameterAttribute((context, add) => add(context.Parameter, attribute()), when, beforeBuildingIndexes, order);

        public void AddParameterAttribute(Func<ParameterModelContext, Attribute> attribute, Func<ParameterModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.AddParameterAttribute((context, add) => add(context.Parameter, attribute(context)), when, beforeBuildingIndexes, order);

        public void AddParameterAttribute(Action<ParameterModelContext, Action<ICustomAttributesModel, Attribute>> apply, Func<ParameterModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) => conventions.Add(new AddAttributeConvention<ParameterModelContext>(apply, when, order.BusinessDefault.Add, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void RemoveParameterAttribute<TAttribute>(Func<ParameterModelContext, bool> when,
            bool beforeBuildingIndexes = true,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new RemoveAttributeConvention<ParameterModelContext, TAttribute>((context, remove) => remove(context.Parameter), when, beforeBuildingIndexes: beforeBuildingIndexes), order.BusinessDefault.Add);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<TypeModelMetadataContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddTypeAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<TypeModelMetadataContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddTypeAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute, TypeModelMetadataContext> attribute,
            Func<TypeModelMetadataContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddTypeAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddTypeAttributeConfiguration<TAttribute>(Action<TAttribute, TypeModelMetadataContext> attribute,
            Func<TypeModelMetadataContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new TypeAttributeConfigurationConvention<TAttribute>(attribute, order.BusinessDefault.Configure, when: when), order: order.BusinessDefault.Configure);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<PropertyModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddPropertyAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<PropertyModelContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddPropertyAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute, PropertyModelContext> attribute,
            Func<PropertyModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddPropertyAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddPropertyAttributeConfiguration<TAttribute>(Action<TAttribute, PropertyModelContext> attribute,
            Func<PropertyModelContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new PropertyAttributeConfigurationConvention<TAttribute>(attribute, order.BusinessDefault.Configure, when: when), order: order.BusinessDefault.Configure);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<MethodModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddMethodAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<MethodModelContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddMethodAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute, MethodModelContext> attribute,
            Func<MethodModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddMethodAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddMethodAttributeConfiguration<TAttribute>(Action<TAttribute, MethodModelContext> attribute,
            Func<MethodModelContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new MethodAttributeConfigurationConvention<TAttribute>(attribute, order.BusinessDefault.Configure, when: when), order: order.BusinessDefault.Configure);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<ParameterModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddParameterAttributeConfiguration<TAttribute>((a, _) => attribute(a), when: when, order: order);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute> attribute,
            Func<ParameterModelContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddParameterAttributeConfiguration((a, _) => attribute(a), when: when, order: order);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute, ParameterModelContext> attribute,
            Func<ParameterModelContext, bool> when, // NOTE this is not optional to avoid ambiguous call when not given
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.AddParameterAttributeConfiguration(attribute, when: (c, _) => when(c), order: order);

        public void AddParameterAttributeConfiguration<TAttribute>(Action<TAttribute, ParameterModelContext> attribute,
            Func<ParameterModelContext, TAttribute, bool>? when = default,
            Order order = default
        ) where TAttribute : Attribute =>
            conventions.Add(new ParameterAttributeConfigurationConvention<TAttribute>(attribute, order.BusinessDefault.Configure, when: when), order: order.BusinessDefault.Configure);
    }

    extension(Stubber _)
    {
        public Id AnId(
            string? starts = default
        )
        {
            starts ??= string.Empty;

            const string template = "4d13bbe0-07a4-4b64-9d31-8fef958fbef1";

            return Id.Parse($"{starts}{template[starts.Length..]}");
        }
    }
}