using Baked.Architecture;
using Baked.ExceptionHandling;
using Baked.ExceptionHandling.ProblemDetails;
using Baked.Runtime;
using Baked.Ui;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Baked;

public static class ProblemDetailsExceptionHandlingFeature
{
    public static IFeature<ExceptionHandlingFeature.Configurator> ProblemDetails(this ExceptionHandlingFeature.Configurator x,
        Setting<string>? typeUrlFormat = default,
        Setting<bool>? showUnhandled = default
    ) => x.Define(configurator =>
    {
        configurator.Runtime.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault("""
            {
              "Logging": {
                "LogLevel": {
                  "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware": "None"
                }
              }
            }
            """);
        });

        configurator.Domain.ConfigureDomainTypeCollection(types =>
        {
            types.Add<HandledException>();
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, AuthenticationExceptionHandler>();
            services.AddSingleton<IExceptionHandler, ClientExceptionHandler>();
            services.AddSingleton<IExceptionHandler, UnauthorizedAccessExceptionHandler>();
            services.AddSingleton<IExceptionHandler, HandledExceptionHandler>();
            services.AddSingleton(new ExceptionHandlerSettings(typeUrlFormat, showUnhandled ?? false));
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app =>
                {
                    app.UseMiddleware<ExceptionLoggerMiddleware>();
                    app.UseExceptionHandler();
                    app.UseStatusCodePages();
                },
                order: -10
            );
        });

        configurator.Ui.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new ErrorHandlingPlugin
            {
                Handlers =
                [
                    new(
                        StatusCode: (int)HttpStatusCode.Unauthorized,
                        Behavior: ErrorHandlingPlugin.HandlerBehavior.Redirect,
                        BehaviorArgument: Fetch.Computed.UseLoginRedirect()
                    ),
                    new(StatusCode: (int)HttpStatusCode.BadRequest, Behavior: ErrorHandlingPlugin.HandlerBehavior.Alert),
                    new(Behavior: ErrorHandlingPlugin.HandlerBehavior.Page),
                ]
            });
        });
    });
}