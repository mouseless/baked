using Baked.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Baked.Localization.AspNetCore;

public class AspNetCoreLocalizationFeature
    : IFeature<LocalizationConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddLocalization(option => option.ResourcesPath = "Resourcess");
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app =>
                {
                    var supportedCultures = new[] { new CultureInfo("en-US") };
                    var localizationOptions = new RequestLocalizationOptions
                    {
                        DefaultRequestCulture = new("en-US"),
                        SupportedCultures = supportedCultures,
                        SupportedUICultures = supportedCultures
                    };
                    app.UseRequestLocalization(localizationOptions);
                },
                order: 10
            );
        });

        // TODO - Burası admin i yönetecek tabi i18n'i
        // configurator.ConfigureAppDescriptor(app =>
        // {
        //     app.Plugins.Add(new ErrorHandlingPlugin()
        //     {
        //         Handlers =
        //         [
        //             new(
        //                 StatusCode: (int)HttpStatusCode.Unauthorized,
        //                 Behavior: ErrorHandlingPlugin.HandlerBehavior.Redirect,
        //                 BehaviorArgument: Datas.Computed("useLoginRedirect")
        //             ),
        //             new(StatusCode: (int)HttpStatusCode.BadRequest, Behavior: ErrorHandlingPlugin.HandlerBehavior.Alert),
        //             new(Behavior: ErrorHandlingPlugin.HandlerBehavior.Page),
        //         ]
        //     });
        // });
    }
}