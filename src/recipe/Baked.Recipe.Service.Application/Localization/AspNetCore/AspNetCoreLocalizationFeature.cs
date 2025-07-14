using Baked.Architecture;
using Baked.Testing;
using Baked.Ui;
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
        configurator.ConfigureGeneratedFileCollection(files =>
        {
            if (configurator.IsNfr())
            {
                return;
            }

            var localeDir = Path.Combine(Assembly.GetEntryAssembly()?.Location ?? throw new("'EntryAssembly' shoul have existed"), "../../../../Locales");

            configurator.UsingLocaleDictionary(locales =>
            {
                files.AddAsJson(FillLocales(_language, localeDir, locales, defaultLanguage: true), name: $"locale.{_language.Name}", outdir: "Ui");

                if (_otherLanguages is not null)
                {
                    foreach (var language in _otherLanguages)
                    {
                        files.AddAsJson(FillLocales(language, localeDir, locales), name: $"locale.{language.Name}", outdir: "Ui");
                    }
                }
            });

            Dictionary<string, string> FillLocales(CultureInfo language, string resourceDir, ILocaleDictionary locales,
                bool defaultLanguage = false
            )
            {
                var result = new Dictionary<string, string>(locales);

                var resourceFilePath = Path.Combine(resourceDir, defaultLanguage ? $"locale.resx" : $"locale.{language.Name}.resx");
                if (File.Exists(resourceFilePath))
                {
                    var values = ReadResxFileAsDictionary(resourceFilePath);
                    foreach (var (key, value) in values)
                    {
                        if (result.ContainsKey(key))
                        {
                            result[key] = value;
                        }
                    }
                }

                return result;
            }

            Dictionary<string, string> ReadResxFileAsDictionary(string path)
            {
                var values = new Dictionary<string, string>();
                using (StreamReader reader = new(path))
                {
                    string? line = reader.ReadLine();
                    while (line != null)
                    {
                        var keyValue = line.Split('=', StringSplitOptions.TrimEntries);
                        values.TryAdd(keyValue[0], keyValue[1]);

                        line = reader.ReadLine();
                    }
                }

                return values;
            }
        });

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
            app.I18n.DefaultLanguage = new(_language.Name, _language.EnglishName);
            app.I18n.SupportedLanguages.Add(app.I18n.DefaultLanguage);

            if (_otherLanguages is not null)
            {
                app.I18n.SupportedLanguages.AddRange(_otherLanguages.Select(l => new Language(l.Name, l.EnglishName)));
            }

            app.Plugins.Add(new LocalizationPlugin()
            {
                SupportedLanguages = app.I18n.SupportedLanguages
            });
        });
    }
}