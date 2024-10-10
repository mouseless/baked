using Baked.Architecture;
using Microsoft.Extensions.Logging;

namespace Baked.Logging.Request;

public class RequestLoggingFeature(bool singleLine)
    : IFeature<LoggingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureConfigurationBuilder(configuration =>
        {
            configuration.AddJsonAsDefault($$"""
            {
              "Logging": {
                "LogLevel": {
                  "Microsoft.AspNetCore.Hosting.Diagnostics": "{{(configurator.IsProduction() ? "Error" : "Information")}}"
                }
              }
            }
            """);
        });

        configurator.ConfigureFluentConfiguration(fluent =>
        {
            fluent.ShowSql(true);
        });

        configurator.ConfigureLoggingBuilder(logging =>
        {
            logging.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = singleLine;
                options.TimestampFormat = "yyyy.MM.dd HH:mm:ss.fff ";
                options.UseUtcTimestamp = true;
            });
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<MappedMethodLogScopeMiddleware>(order: -20);
        });
    }
}