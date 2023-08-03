using Do.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

using static Do.Web.WebLayer;

namespace Do.Logging;

public class MonitoringLayer : LayerBase<CreateBuilder>
{
    protected override PhaseContext GetContext(CreateBuilder phase)
    {
        var builder = Context.Get<WebApplicationBuilder>();

        builder.Logging.ClearProviders();

        return phase.CreateContext(builder.Logging);
    }
}
