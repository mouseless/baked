using Baked.Architecture;
using Baked.Testing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace Baked.Localization.AspNetCore;

public class AspNetCoreLocalizationFeature(CultureInfo _language,
    IEnumerable<CultureInfo>? _otherLanguages = default
) : IFeature<LocalizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddLocalization(option => option.ResourcesPath = "Locales");
            var entryAssembly = (configurator.IsNfr() ? Nfr.EntryAssembly : Assembly.GetEntryAssembly())
                ?? throw new("'EntryAssembly' should have existed");
            var entryAssemblyName = entryAssembly.GetName().Name
                ?? throw new("'EntryAssembly' should have a name");

            services.AddSingleton<ILocalizer>(provider =>
            {
                var factory = provider.GetRequiredService<IStringLocalizerFactory>();
                var localizer = factory.Create("locale", entryAssemblyName);

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
                order: -20 // should be added before ExceptionHandlingMiddleware
            );
        });

        configurator.ConfigureSwaggerGenOptions(swg =>
        {
            swg.OperationFilter<LocalizationOperationFilter>();
        });

        configurator.ConfigureAppDescriptor(app =>
        {
            app.I18n.DefaultLanguage = new(_language.Name, _language.DisplayName);
            app.I18n.SupportedLanguages.Add(app.I18n.DefaultLanguage);

            if (_otherLanguages is not null)
            {
                app.I18n.SupportedLanguages.AddRange(_otherLanguages.Select(l => new Language(l.Name, l.DisplayName)));
            }

            app.Plugins.Add(new LocalizationPlugin()
            {
                SupportedLanguages = app.I18n.SupportedLanguages
            });
        });
    }
}