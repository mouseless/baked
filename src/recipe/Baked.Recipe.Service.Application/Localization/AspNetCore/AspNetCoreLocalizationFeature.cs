using Baked.Architecture;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace Baked.Localization.AspNetCore;

public class AspNetCoreLocalizationFeature(Setting<string>? _resourceName, IEnumerable<string>? _supportedLanguages)
    : IFeature<LocalizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            var entryAssembly = Assembly.GetEntryAssembly() ?? throw new("'EntryAssembly' should have existed");
            var resourceName = _resourceName ?? "$";
            services.AddSingleton(provider =>
                provider.GetRequiredService<IStringLocalizerFactory>().Create(resourceName, entryAssembly.GetName().Name!)
            );
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app =>
                {
                    var supportedCultures = _supportedLanguages is null
                        ? [new CultureInfo("en")]
                        : _supportedLanguages.Select(l => new CultureInfo(l)).ToList();
                    var localizationOptions = new RequestLocalizationOptions
                    {
                        DefaultRequestCulture = new("en"),
                        SupportedCultures = supportedCultures,
                        SupportedUICultures = supportedCultures
                    };
                    app.UseRequestLocalization(localizationOptions);
                },
                order: 10
            );
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new LocalizationPlugin()
            {
                SupportedLanguages = _supportedLanguages ?? ["en"]
            });
        });
    }
}