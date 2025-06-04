using Baked.Architecture;
using Baked.Runtime;
using Baked.Testing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace Baked.Localization.AspNetCore;

public class AspNetCoreLocalizationFeature(Setting<string>? _resourceName, CultureInfo _language,
    IEnumerable<CultureInfo>? _otherLanguages = default
) : IFeature<LocalizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            var entryAssembly = (configurator.IsNfr() ? Nfr.EntryAssembly : Assembly.GetEntryAssembly())
                ?? throw new("'EntryAssembly' should have existed");
            var resourceName = _resourceName ?? "locale";
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
                    List<CultureInfo> supportedCultures = [_language, .. _otherLanguages ?? []];
                    var localizationOptions = new RequestLocalizationOptions
                    {
                        DefaultRequestCulture = new(_language.Name),
                        SupportedCultures = supportedCultures,
                        SupportedUICultures = supportedCultures
                    };
                    app.UseRequestLocalization(localizationOptions);
                },
                order: -20
            );
        });

        configurator.ConfigureSwaggerGenOptions(swg =>
        {
            swg.OperationFilter<LocalizationOperationFilter>();
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            var supportedLanguages = new List<SupportedLanguage> { new(_language.Name, _language.DisplayName) };
            if (_otherLanguages is not null)
            {
                supportedLanguages.AddRange(_otherLanguages.ToList().Select(l => new SupportedLanguage(l.Name, l.DisplayName)));
            }

            app.Plugins.Add(new LocalizationPlugin()
            {
                SupportedLanguages = supportedLanguages
            });

            app.I18n = new(supportedLanguages);
        });
    }
}