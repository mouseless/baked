using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Baked.Core.Dotnet;

public class DotnetCoreFeature(
    Func<string?>? _baseNamespace = default
) : IFeature<CoreConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(TimeProvider.System);

            _baseNamespace ??= () => Assembly.GetExecutingAssembly().FullName;

            services.AddFileProvider(new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), _baseNamespace()));
            services.AddFileProvider(new PhysicalFileProvider(Path.GetDirectoryName(Assembly.GetExecutingAssembly()?.Location) ??
                throw new("'Assembly.GetEntryAssembly()' should not be null"))
            );
        });
    }
}