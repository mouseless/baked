using Baked.Architecture;
using Baked.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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

        configurator.ConfigureDomainServicesModel(model =>
        {
            var domain = configurator.Context.GetDomainModel();
            var exceptionHandlerTypes = domain.Types.Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo<IExceptionHandler>());

            foreach (var exceptionHandlerType in exceptionHandlerTypes)
            {
                model.Services.Add(new(
                   ServiceType: exceptionHandlerType,
                   Lifetime: ServiceLifetime.Singleton,
                   UseFactory: false,
                   Interfaces: !exceptionHandlerType.TryGetInheritance(out var inheritance) ? [] : inheritance.Interfaces.Where(i => i.Model.Is<IExceptionHandler>()),
                   Forward: true
               ));

                exceptionHandlerType.Apply(t => model.References.Add(t.Assembly));

                model.Usings.AddRange([
                    "Baked.Business",
                    "Baked.ExceptionHandling",
                    "Baked.Runtime",
                    "Microsoft.Extensions.DependencyInjection"
                ]);
            }

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
    }
}