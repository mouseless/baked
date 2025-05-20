using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Model;
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
            metadata.TryGetSingle(out namespaceAttribute);
    }
}