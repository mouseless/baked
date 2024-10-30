using Baked.Architecture;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked.Core.Dotnet;

public class DotnetCoreFeature(Assembly? _entryAssembly, Func<Assembly, string?> _baseNamespace)
    : IFeature<CoreConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            var entryAssembly = _entryAssembly
                ?? (configurator.IsNfr() ? Nfr.EntryAssembly : Assembly.GetEntryAssembly())
                ?? throw new("'EntryAssembly' should have existed");

            services.AddSingleton(TimeProvider.System);
            services.AddSingleton<ITextTransformer, HumanizerTextTransformer>();

            services.AddFileProvider(new EmbeddedFileProvider(entryAssembly, _baseNamespace(entryAssembly)));
            services.AddFileProvider(new PhysicalFileProvider(Path.GetDirectoryName(entryAssembly.Location) ?? string.Empty));
        });
    }
}