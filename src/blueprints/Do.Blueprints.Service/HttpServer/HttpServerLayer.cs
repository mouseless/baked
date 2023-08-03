using Do.Architecture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

using static Do.DependencyInjection.DependencyInjectionLayer;
using static Do.HttpServer.HttpServerLayer;

namespace Do.HttpServer;

public class HttpServerLayer : LayerBase<AddServices, Build>
{
    readonly IMiddlewareCollection _middlewares = new MiddlewareCollection();

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();

        services.AddHttpContextAccessor();

        return PhaseContext.Empty;
    }

    protected override PhaseContext GetContext(Build phase) =>
        phase.CreateContextBuilder()
            .Add<IMiddlewareCollection>(_middlewares)
            .Add<IEndpointRouteBuilder>(Context.GetWebApplication())
            .Build();

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new Build();
        yield return new Run(_middlewares);
    }

    public class CreateBuilder : PhaseBase
    {
        public CreateBuilder() : base(PhaseOrder.Earliest) { }

        protected override void Initialize()
        {
            var build = WebApplication.CreateBuilder();

            Context.Add(build);
        }
    }

    public class Build : PhaseBase<WebApplicationBuilder, IServiceCollection>
    {
        public Build() : base(PhaseOrder.Latest) { }

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

    class Run : PhaseBase<WebApplication>
    {
        readonly IMiddlewareCollection _middlewares;

        public Run(IMiddlewareCollection middlewares) : base(PhaseOrder.Latest) =>
            _middlewares = middlewares;

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
