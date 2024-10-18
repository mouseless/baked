using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Baked.Core.Dotnet;

public class DotnetCoreFeature(
    Assembly? entryAssembly = default,
    string? _baseNamespace = default
) : IFeature<CoreConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton(TimeProvider.System);

            entryAssembly ??= Assembly.GetEntryAssembly() ?? throw new("'EntryAssembly' should not be null");
            _baseNamespace ??= entryAssembly.FullName ?? string.Empty;
            _baseNamespace = Regex.Match(_baseNamespace, @"[\s\S]*?(?=.Application|$)").Value;

            services.AddFileProvider(new EmbeddedFileProvider(entryAssembly, _baseNamespace));
            services.AddFileProvider(new PhysicalFileProvider(Path.GetDirectoryName(entryAssembly.Location) ??
                throw new("'EntryAssembly' should have a not null location"))
            );
        });
    }
}