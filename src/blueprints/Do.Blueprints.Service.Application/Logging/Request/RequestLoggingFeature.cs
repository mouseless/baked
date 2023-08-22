using Do.Architecture;
using Microsoft.Extensions.Logging;

namespace Do.Logging.Request;

public class RequestLoggingFeature : IFeature, ILoggingFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureLoggingBuilder(logging =>
        {
            logging.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
            });
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<RequestLogMiddleware>(order: -10);
        });
    }
}
