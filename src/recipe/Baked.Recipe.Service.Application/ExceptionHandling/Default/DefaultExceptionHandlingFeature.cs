using Baked.Architecture;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.ExceptionHandling.Default;

public class DefaultExceptionHandlingFeature(Setting<string>? _typeUrlFormat = default)
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

            var domainModel = configurator.Context.GetDomainModel();
            foreach (var exceptionHandlerType in domainModel.Types.Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo<IExceptionHandler>()))
            {
                exceptionHandlerType.Apply(t =>
                {
                    services.AddSingleton(typeof(IExceptionHandler), t, forward: true);
                });
            }

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
    }
}