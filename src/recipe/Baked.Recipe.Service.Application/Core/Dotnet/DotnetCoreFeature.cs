using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked.Core.Dotnet;

public class DotnetCoreFeature(Assembly _entryAssembly, Func<Assembly, string?> _baseNamespace) : IFeature<CoreConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(TimeProvider.System);

            services.AddFileProvider(new EmbeddedFileProvider(_entryAssembly, _baseNamespace(_entryAssembly)));
            services.AddFileProvider(new PhysicalFileProvider(Path.GetDirectoryName(_entryAssembly.Location) ??
                throw new("'EntryAssembly' should have a not null location"))
            );
        });
    }
}