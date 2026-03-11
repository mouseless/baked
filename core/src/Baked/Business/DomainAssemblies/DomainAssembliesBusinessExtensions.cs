using Baked.Business;
using Baked.Business.DomainAssemblies;
using Baked.Domain.Model;
using System.Reflection;

namespace Baked;

public static class DomainAssembliesBusinessExtensions
{
    extension(BusinessConfigurator configurator)
    {
        public DomainAssembliesBusinessFeature DomainAssemblies(Assembly assembly,
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

        public DomainAssembliesBusinessFeature DomainAssemblies(IEnumerable<Assembly> assemblies,
            Func<Assembly, string>? baseNamespace = default,
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

        public DomainAssembliesBusinessFeature DomainAssemblies(IEnumerable<(Assembly assembly, string baseNamespace)> assemblyDescriptors,
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
    }

    extension(IEnumerable<MethodOverloadModel> overloads)
    {
        public MethodOverloadModel? FirstPublicInstanceWithMostParametersOrDefault() =>
            overloads
                .Where(o => o.IsPublic && !o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstNonPublicInstanceWithMostParametersOrDefault() =>
            overloads
                .Where(o => !o.IsPublic && !o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstPublicStaticWithMostParametersOrDefault() =>
            overloads
                .Where(o => o.IsPublic && o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstNonPublicStaticWithMostParametersOrDefault() =>
            overloads
                .Where(o => !o.IsPublic && o.IsStatic)
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();

        public MethodOverloadModel? FirstWithMostParametersOrDefault() =>
            overloads
                .OrderByDescending(o => o.Parameters.Count)
                .FirstOrDefault();
    }
}