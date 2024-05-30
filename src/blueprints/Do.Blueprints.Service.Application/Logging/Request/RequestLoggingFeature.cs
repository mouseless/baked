using Do.Architecture;
using Microsoft.Extensions.Logging;

namespace Do.Logging.Request;

public class RequestLoggingFeature(bool singleLine)
    : IFeature<LoggingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
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

        configurator.ConfigureApiModelConventions(conventions =>
        {
            conventions.Add(new AddMappedMethodAttributeConvention());
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<RequestLogMiddleware>(order: -20);
        });
    }
}