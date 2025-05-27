using Baked.Architecture;
using Baked.Runtime;
using Baked.Testing;
using Baked.Ui;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace Baked.Localization.AspNetCore;

public class AspNetCoreLocalizationFeature(Setting<string>? _resourceName, IEnumerable<SupportedLanguage>? _supportedLanguages)
    : IFeature<LocalizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            var entryAssembly = (configurator.IsNfr() ? Nfr.EntryAssembly : Assembly.GetEntryAssembly())
                ?? throw new("'EntryAssembly' should have existed");
            var resourceName = _resourceName ?? "$";
            services.AddSingleton<ILocalizer>(provider =>
            {
                var factory = provider.GetRequiredService<IStringLocalizerFactory>();
                var localizer = factory.Create(resourceName, entryAssembly.GetName().Name!);

                return new LocalizerAdapter(localizer);
            });
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app =>
                {
                    var supportedCultures = _supportedLanguages is null
                        ? [new CultureInfo("en")]
                        : _supportedLanguages.Select(l => new CultureInfo(l.Code)).ToList();
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

        configurator.ConfigureSwaggerGenOptions(swg =>
        {
            swg.OperationFilter<LocalizationOperationFilter>();
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Localization = new LocalizationDescriptor()
            {
                SupportedLanguages = _supportedLanguages ?? [new("en", "English")]
            };
        });
    }
}