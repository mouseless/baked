using Do.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using static Do.HttpServer.HttpServerLayer;

namespace Do.HttpServer;

public class HttpServerLayer : LayerBase<Build>
{
    readonly IMiddlewareCollection _middlewares = new MiddlewareCollection();

    protected override PhaseContext GetContext(Build phase) =>
        phase.CreateContextBuilder()
            .Add(_middlewares)
            .Add<IEndpointRouteBuilder>(Context.GetWebApplication())
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new Build();
        yield return new Run(_middlewares);
    }

    public class CreateBuilder()
        : PhaseBase(PhaseOrder.Earliest)
    {
        protected override void Initialize()
        {
            var build = WebApplication.CreateBuilder();

            Context.Add(build);
            Context.Add(build.Configuration);
        }
    }

    public class Build()
        : PhaseBase<WebApplicationBuilder, IServiceCollection>(PhaseOrder.Latest)
    {
        protected override void Initialize(WebApplicationBuilder build, IServiceCollection services)
        {
            foreach (var service in services)
            {
                build.Services.Add(service);
            }

            var app = build.Build();

            Context.Add(app);
        }
    }

    class Run(IMiddlewareCollection _middlewares)
        : PhaseBase<WebApplication>(PhaseOrder.Latest)
    {
        protected override void Initialize(WebApplication app)
        {
            foreach (var middleware in _middlewares.OrderBy(m => m.Order))
            {
                middleware.Configure(app);
            }

            app.Run();
        }
    }
}