using Baked.Architecture;
using Baked.Runtime;
using Baked.Theme.Admin;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ProblemDetailsExceptionHandlingFeature(Setting<string>? _typeUrlFormat = default)
    : IFeature<ExceptionHandlingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureConfigurationBuilder(configuration =>
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

        configurator.ConfigureDomainTypeCollection(types =>
        {
            types.Add<HandledException>();
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, AuthenticationExceptionHandler>();
            services.AddSingleton<IExceptionHandler, UnauthorizedAccessExceptionHandler>();
            services.AddSingleton<IExceptionHandler, HandledExceptionHandler>();
            services.AddSingleton(new ExceptionHandlerSettings(_typeUrlFormat));
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
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

        configurator.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new ErrorHandlingPlugin()
            {
                Handlers = new()
                {
                    new(StatusCode: (int)HttpStatusCode.Unauthorized, Behavior: ErrorHandlingPlugin.HandlerBehavior.Redirect, BehaviorArgument: "/"),
                    new(StatusCode: (int)HttpStatusCode.BadRequest, Behavior: ErrorHandlingPlugin.HandlerBehavior.Alert),
                    new(Behavior: ErrorHandlingPlugin.HandlerBehavior.Page),
                }
            });
        });
    }
}