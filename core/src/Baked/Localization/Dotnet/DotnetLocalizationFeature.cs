using Baked.Architecture;
using Baked.Testing;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace Baked.Localization.Dotnet;

public class DotnetLocalizationFeature(CultureInfo _language,
    IEnumerable<CultureInfo>? _otherLanguages = default
) : IFeature<LocalizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Buildtime.ConfigureGeneratedFileCollection(files =>
        {
            if (configurator.IsNfr) { return; }

            var localeDir = Path.Combine(
                Assembly.GetEntryAssembly()?.Location ??
                throw new("'EntryAssembly' should have existed"), "../../../../Locales"
            );

            configurator.Ui.UsingLocaleTemplate(localeTemplate =>
            {
                files.AddAsJson(new LocalizedTexts(_language, localeTemplate).With(localeDir),
                    name: $"locale.{_language.Name}",
                    outdir: "Ui"
                );
                foreach (var language in _otherLanguages ?? [])
                {
                    files.AddAsJson(new LocalizedTexts(language, localeTemplate).With(localeDir),
                        name: $"locale.{language.Name}",
                        outdir: "Ui"
                    );
                }
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            var entryAssembly =
                (configurator.IsNfr ? Nfr.EntryAssembly : Assembly.GetEntryAssembly()) ??
                throw new("'EntryAssembly' should have existed");
            var entryAssemblyName =
                entryAssembly.GetName().Name ??
                throw new("'EntryAssembly' should have a name");

            services.AddLocalization(option => option.ResourcesPath = "Locales");
            services.AddSingleton(sp => sp.GetRequiredService<IStringLocalizerFactory>().Create("locale", entryAssemblyName));

            // Configures error messages from attributes
            services.AddSingleton<LocalizedValidationMetadataProvider>();
            services.Configure<MvcDataAnnotationsLocalizationOptions>(opts =>
                opts.DataAnnotationLocalizerProvider = (_, factory) => factory.Create("locale", entryAssemblyName)
            );
            services.Configure<MvcOptions, LocalizedValidationMetadataProvider>((opts, lvmp) =>
                opts.ModelMetadataDetailsProviders.Add(lvmp)
            );

            // Configures problem details object for model validation errors
            services.Configure<ApiBehaviorOptions>(opts =>
            {
                opts.InvalidModelStateResponseFactory = ctx =>
                {
                    var l = ctx.HttpContext.RequestServices.GetRequiredService<IStringLocalizer>();
                    var errors = ctx.ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? []
                    );
                    var details = new ValidationProblemDetails(ctx.ModelState)
                    {
                        Title = l["Invalid Request"],
                        Status = 400,
                        Errors = errors
                    };

                    return new BadRequestObjectResult(details);
                };
            });

            // Configures localization for model binding errors
            services.Configure<MvcOptions, IStringLocalizer>((opts, l) =>
            {
                opts.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((val, field) =>
                    l["The value '{0}' is not valid for the field '{1}'.", val, l[field.Pascalize()]]
                );
                opts.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(field =>
                    l["A value for '{0}' field was not provided.", l[field.Pascalize()]]
                );
            });
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
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

        configurator.RestApi.ConfigureSwaggerGenOptions(swg =>
        {
            swg.OperationFilter<LocalizationOperationFilter>();
        });

        configurator.Ui.ConfigureAppDescriptor(app =>
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