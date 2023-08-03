using Do.Architecture;
using Microsoft.Extensions.Logging;

using static Do.HttpServer.HttpServerLayer;

namespace Do.Logging;

public class MonitoringLayer : LayerBase<CreateBuilder>
{
    protected override PhaseContext GetContext(CreateBuilder phase)
    {
        var builder = Context.GetWebApplicationBuilder();

        builder.Logging.ClearProviders();

        return phase.CreateContext(builder.Logging);
    }
}
