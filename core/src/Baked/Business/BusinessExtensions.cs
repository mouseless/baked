using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Model;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
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

    extension(DiagnosticsCode)
    {
        public static DiagnosticsCode TypeWithAttribute => new(001, "type-with-attribute");
        public static DiagnosticsCode RequiresBuildLevel => new(002, "requires-build-level");
        public static DiagnosticsCode PropertyWithAttribute => new(003, "property-with-attribute");
        public static DiagnosticsCode MethodWithAttribute => new(004, "method-with-attribute");
        public static DiagnosticsCode ParameterWithAttribute => new(005, "parameter-with-attribute");
        public static DiagnosticsCode RequiresElementType => new(006, "requires-element-type");
    }

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
                Diagnostics.ReportError(
                    DiagnosticsCode.RequiresBuildLevel,
                    $"{type.Name} doesn't provide generics information to skip nullable"
                );
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
        ) where TAttribute : Attribute
        {
            var result = members.FirstPropertyOrDefault<TAttribute>(filter: filter);
            if (result is null)
            {
                Diagnostics.ReportError(
                    DiagnosticsCode.PropertyWithAttribute,
                    $"{members.Name} is expected to have at least one property with `{typeof(TAttribute).Name}`"
                );
            }

            return result;
        }

        public PropertyModel? FirstPropertyOrDefault<TAttribute>(
            Func<PropertyModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            members.Properties.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));

        public MethodModel FirstMethod<TAttribute>(
            Func<MethodModel, bool>? filter = default
        ) where TAttribute : Attribute
        {
            var result = members.FirstMethodOrDefault<TAttribute>(filter: filter);
            if (result is null)
            {
                Diagnostics.ReportError(
                    DiagnosticsCode.MethodWithAttribute,
                    $"{members.Name} is expected to have at least one method with `{typeof(TAttribute).Name}`"
                );
            }

            return result;
        }

        public MethodModel? FirstMethodOrDefault<TAttribute>(
            Func<MethodModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            members.Methods.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));
    }

    extension(MethodModel method)
    {
        public ParameterModel FirstParameter<TAttribute>(
            Func<ParameterModel, bool>? filter = default
        ) where TAttribute : Attribute
        {
            var result = method.FirstParameterOrDefault<TAttribute>(filter: filter);
            if (result is null)
            {
                Diagnostics.ReportError(
                    DiagnosticsCode.ParameterWithAttribute,
                    $"{method.Name} is expected to have at least one parameter with `{typeof(TAttribute).Name}`"
                );
            }

            return result;
        }

        public ParameterModel? FirstParameterOrDefault<TAttribute>(
            Func<ParameterModel, bool>? filter = default
        ) where TAttribute : Attribute =>
            method.DefaultOverload.Parameters.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));
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

    extension(Type type)
    {
        public Type SkipNullable() =>
            Nullable.GetUnderlyingType(type) ?? type;
    }
}