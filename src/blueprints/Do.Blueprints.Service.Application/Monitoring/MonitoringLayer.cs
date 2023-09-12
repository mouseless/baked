using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using static Do.DependencyInjection.DependencyInjectionLayer;
using static Do.HttpServer.HttpServerLayer;

namespace Do.Logging;

public class MonitoringLayer : LayerBase<CreateBuilder, AddServices>
{
    protected override PhaseContext GetContext(CreateBuilder phase)
    {
        var builder = Context.GetWebApplicationBuilder();

        builder.Logging.ClearProviders();

        return phase.CreateContext(builder.Logging);
    }

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();

        services.AddLogging();

        return PhaseContext.Empty;
    }
}
