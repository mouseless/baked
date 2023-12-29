using Do.Architecture;
using Do.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Do.ExceptionHandling.Default;

public class DefaultExceptionHandlingFeature(Setting<string>? _exceptionTypeUrl = default)
    : IFeature<ExceptionHandlingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, HandledExceptionHandler>();
            services.AddSingleton<Func<ExceptionConfig>>(sp => () => new(_exceptionTypeUrl?.GetValue()));

            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app =>
                {
                    app.UseExceptionHandler();
                    app.UseStatusCodePages();
                }
            );
        });
    }
}
