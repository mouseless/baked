using Baked.Architecture;
using Baked.Core;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked;

public static class DotnetCoreFeature
{
    public static IFeature<CoreFeature.Configurator> Dotnet(this CoreFeature.Configurator x,
        Assembly? entryAssembly = default,
        string? baseNamespace = default
    ) => x.Define(configurator =>
    {
        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            var assembly = entryAssembly
                ?? (configurator.IsNfr ? Nfr.EntryAssembly : Assembly.GetEntryAssembly())
                ?? throw new("'EntryAssembly' should have existed");

            services.AddSingleton(TimeProvider.System);
            services.AddSingleton<ITextTransformer, HumanizerTextTransformer>();

            services.AddFileProvider(new EmbeddedFileProvider(assembly, baseNamespace ?? assembly.GetNameBeforeApplicationSuffix()));
            services.AddFileProvider(new PhysicalFileProvider(Path.GetDirectoryName(assembly.Location) ?? string.Empty));
        });
    });
}