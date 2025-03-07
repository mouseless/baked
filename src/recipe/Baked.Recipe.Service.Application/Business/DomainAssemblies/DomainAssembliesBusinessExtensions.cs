using Baked.Business;
using Baked.Business.DomainAssemblies;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked;

public static class DomainAssembliesBusinessExtensions
{
    public static DomainAssembliesBusinessFeature DomainAssemblies(this BusinessConfigurator configurator, Assembly assembly,
        string? baseNamespace = default,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true,
        Func<TypeModel, bool>? setNamespaceWhen = default
    ) => configurator.DomainAssemblies([assembly],
        baseNamespace: baseNamespace is not null ? _ => baseNamespace : null,
        defaultOverloadSelector: defaultOverloadSelector,
        addEmbeddedFileProviders: addEmbeddedFileProviders,
        setNamespaceWhen: setNamespaceWhen
    );

    public static DomainAssembliesBusinessFeature DomainAssemblies(this BusinessConfigurator configurator, IEnumerable<Assembly> assemblies,
        Func<Assembly, string>? baseNamespace,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true,
        Func<TypeModel, bool>? setNamespaceWhen = default
    )
    {
        baseNamespace ??= (a => a.GetName().Name ?? string.Empty);

        return configurator.DomainAssemblies(assemblies.Select(a => (a, baseNamespace(a))),
            defaultOverloadSelector: defaultOverloadSelector,
            addEmbeddedFileProviders: addEmbeddedFileProviders,
            setNamespaceWhen: setNamespaceWhen
        );
    }

    public static DomainAssembliesBusinessFeature DomainAssemblies(this BusinessConfigurator _, IEnumerable<(Assembly assembly, string baseNamespace)> assemblyDescriptors,
        Func<IEnumerable<MethodOverloadModel>, MethodOverloadModel>? defaultOverloadSelector = default,
        bool addEmbeddedFileProviders = true,
        Func<TypeModel, bool>? setNamespaceWhen = default
    ) => new(
        assemblyDescriptors,
        defaultOverloadSelector ?? (overloads =>
            overloads.FirstPublicInstanceWithMostParametersOrDefault() ??
            overloads.FirstNonPublicInstanceWithMostParametersOrDefault() ??
            overloads.FirstPublicStaticWithMostParametersOrDefault() ??
            overloads.FirstNonPublicStaticWithMostParametersOrDefault() ??
            overloads.FirstWithMostParametersOrDefault() ??
            throw new($"Method without an overload should not exist")
        ),
        addEmbeddedFileProviders,
        setNamespaceWhen ?? (t => true)
    );

    public static MethodOverloadModel? FirstPublicInstanceWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => o.IsPublic && !o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstNonPublicInstanceWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => !o.IsPublic && !o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstPublicStaticWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => o.IsPublic && o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstNonPublicStaticWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .Where(o => !o.IsPublic && o.IsStatic)
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();

    public static MethodOverloadModel? FirstWithMostParametersOrDefault(this IEnumerable<MethodOverloadModel> overloads) =>
        overloads
            .OrderByDescending(o => o.Parameters.Count)
            .FirstOrDefault();
}