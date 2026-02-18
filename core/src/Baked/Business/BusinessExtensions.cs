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
    public static void AddBusiness(this List<IFeature> features, Func<BusinessConfigurator, IFeature<BusinessConfigurator>> configure) =>
        features.Add(configure(new()));

    static readonly MethodInfo _addTransientWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddTransientWithFactory), 2, [typeof(IServiceCollection)]) ??
        throw new("AddTransientWithFactory<TService, TImplementation> should have existed");

    static readonly MethodInfo _addScopedWithFactory = typeof(BusinessExtensions).GetMethod(nameof(AddScopedWithFactory), 2, [typeof(IServiceCollection)]) ??
        throw new("AddScopedWithFactory<TService, TImplementation> should have existed");

    public static IServiceCollection AddTransientWithFactory(this IServiceCollection services, Type service) =>
        (IServiceCollection?)_addTransientWithFactory.MakeGenericMethod(service, service).Invoke(null, [services]) ??
        throw new("Should've returned an IServiceCollection instance");
    public static IServiceCollection AddTransientWithFactory(this IServiceCollection services, Type service, Type implementation) =>
        (IServiceCollection?)_addTransientWithFactory.MakeGenericMethod(service, implementation).Invoke(null, [services]) ??
        throw new("Should've returned an IServiceCollection instance");

    public static IServiceCollection AddTransientWithFactory<TService>(this IServiceCollection services) where TService : class =>
        services.AddTransientWithFactory<TService, TService>();

    public static IServiceCollection AddTransientWithFactory<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    => services
        .AddSingleton<Func<TService>>(sp => () => sp.UsingCurrentScope().GetRequiredService<TService>())
        .AddTransient<TService, TImplementation>();

    public static IServiceCollection AddScopedWithFactory(this IServiceCollection services, Type service) =>
        (IServiceCollection?)_addScopedWithFactory.MakeGenericMethod(service, service).Invoke(null, [services]) ??
        throw new("Should've returned an IServiceCollection instance");
    public static IServiceCollection AddScopedWithFactory(this IServiceCollection services, Type service, Type implementation) =>
        (IServiceCollection?)_addScopedWithFactory.MakeGenericMethod(service, implementation).Invoke(null, [services]) ??
        throw new("Should've returned an IServiceCollection instance");
    public static void AddScopedWithFactory<TService>(this IServiceCollection services) where TService : class =>
        services.AddScopedWithFactory<TService, TService>();

    public static IServiceCollection AddScopedWithFactory<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    => services
        .AddSingleton<Func<TService>>(sp => () => sp.UsingCurrentScope().GetRequiredService<TService>())
        .AddScoped<TService, TImplementation>();

    public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, bool forward)
        where TService : class
        where TImplementation : class, TService
    => services.AddSingleton(typeof(TService), typeof(TImplementation), forward: forward);

    public static IServiceCollection AddSingleton(this IServiceCollection services, Type service, Type implementation, bool forward)
    {
        if (!forward) { return services.AddSingleton(service, implementation); }

        return services.AddSingleton(service, sp => sp.UsingCurrentScope().GetRequiredService(implementation));
    }

    public static bool TryGetNamespace(this TypeModel type, [NotNullWhen(true)] out string? @namespace)
    {
        if (!type.TryGetNamespaceAttribute(out var namespaceAttribute) || string.IsNullOrWhiteSpace(namespaceAttribute.Value))
        {
            @namespace = null;

            return false;
        }

        @namespace = namespaceAttribute.Value;

        return true;
    }

    public static bool TryGetNamespaceAttribute(this TypeModel type, [NotNullWhen(true)] out NamespaceAttribute? namespaceAttribute)
    {
        namespaceAttribute = default;

        return
            type.TryGetMetadata(out var metadata) &&
            metadata.TryGet(out namespaceAttribute);
    }

    public static PropertyModel FirstProperty<TAttribute>(this TypeModelMembers members,
        Func<PropertyModel, bool>? filter = default
    ) where TAttribute : Attribute =>
        members.FirstPropertyOrDefault<TAttribute>(filter: filter) ??
        throw new($"{members.Name} is expected to have at least one property with `{typeof(TAttribute).Name}`");

    public static PropertyModel? FirstPropertyOrDefault<TAttribute>(this TypeModelMembers members,
        Func<PropertyModel, bool>? filter = default
    ) where TAttribute : Attribute =>
        members.Properties.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));

    public static MethodModel FirstMethod<TAttribute>(this TypeModelMembers members,
        Func<MethodModel, bool>? filter = default
    ) where TAttribute : Attribute =>
        members.FirstMethodOrDefault<TAttribute>(filter: filter) ??
        throw new($"{members.Name} is expected to have at least one method with `{typeof(TAttribute).Name}`");

    public static MethodModel? FirstMethodOrDefault<TAttribute>(this TypeModelMembers members,
        Func<MethodModel, bool>? filter = default
    ) where TAttribute : Attribute =>
        members.Methods.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));

    public static ParameterModel FirstParameter<TAttribute>(this MethodModel method,
        Func<ParameterModel, bool>? filter = default
    ) where TAttribute : Attribute =>
        method.FirstParameterOrDefault<TAttribute>(filter: filter) ??
        throw new($"{method.Name} is expected to have at least one parameter with `{typeof(TAttribute).Name}`");

    public static ParameterModel? FirstParameterOrDefault<TAttribute>(this MethodModel method,
        Func<ParameterModel, bool>? filter = default
    ) where TAttribute : Attribute =>
        method.DefaultOverload.Parameters.Having<TAttribute>().FirstOrDefault(filter ?? (_ => true));

    public static bool HasIdInfo(this TypeModel type) =>
        type.TryGetIdInfo(out var _);

    public static IdInfo GetIdInfo(this TypeModel type)
    {
        if (!type.TryGetIdInfo(out var result))
        {
            throw new InvalidOperationException($"`{type.Name}` does not have `IdentifierInfo`");
        }

        return result;
    }

    public static bool TryGetIdInfo(this TypeModel type, [NotNullWhen(true)] out IdInfo? idInfo)
    {
        idInfo = null;

        if (!type.TryGetMembers(out var members)) { return false; }

        var idProperty = members.FirstPropertyOrDefault<IdAttribute>();
        if (idProperty is null) { return false; }

        idInfo = new(idProperty.PropertyType.CSharpFriendlyFullName, idProperty.Name, idProperty.Get<IdAttribute>().RouteName);

        return true;
    }

    public static Id AnId(this Stubber _,
        string? starts = default
    )
    {
        starts ??= string.Empty;

        const string template = "4d13bbe0-07a4-4b64-9d31-8fef958fbef1";

        return Id.Parse($"{starts}{template[starts.Length..]}");
    }

    public static TypeModel SkipNullable(this TypeModel type)
    {
        if (!type.IsAssignableTo(typeof(Nullable<>))) { return type; }
        if (!type.TryGetGenerics(out var generics)) { throw new InvalidOperationException($"{type.Name} doesn't provide generics information"); }
        if (type.IsGenericTypeDefinition) { return type; }

        return generics.GenericTypeArguments.First().Model;
    }

    public static Type SkipNullable(this Type type) =>
        Nullable.GetUnderlyingType(type) ?? type;

    public static TypeModel SkipTask(this TypeModel typeModel) =>
        typeModel.IsAssignableTo<Task>() && typeModel.IsGenericType
            ? typeModel.GetGenerics().GenericTypeArguments.First().Model
            : typeModel;
}