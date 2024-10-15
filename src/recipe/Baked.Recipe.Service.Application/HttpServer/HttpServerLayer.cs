using Baked.Architecture;
using Baked.Runtime;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

using static Baked.HttpServer.HttpServerLayer;
using static Baked.Runtime.RuntimeLayer;

namespace Baked.HttpServer;

public class HttpServerLayer : LayerBase<AddServices, Build>
{
    readonly IAuthenticationCollection _authentications = new AuthenticationCollection();
    readonly IMiddlewareCollection _middlewares = new MiddlewareCollection();

    protected override PhaseContext GetContext(AddServices phase)
    {
        var services = Context.GetServiceCollection();
        services.AddHttpContextAccessor();
        services.AddSingleton<Func<ClaimsPrincipal>>(sp => () => sp.GetRequiredService<IHttpContextAccessor>().HttpContext?.User ?? throw new("HttpContext.User is required"));
        services.AddSingleton<IServiceProviderAccessor, RequestServicesServiceProviderAccessor>();

        return phase.CreateContextBuilder()
            .Add(_authentications)
            .OnDispose(() =>
            {
                if (_authentications.Any())
                {
                    var builder = services.AddAuthentication(options =>
                    {
                        options.DefaultScheme = "Default";
                        options.AddScheme<DefaultAuthenticationHandler>("Default", "Default");
                    });

                    foreach (var scheme in _authentications)
                    {
                        scheme.UseBuilder.Invoke(builder);
                    }

                    services.Configure<AuthenticationSchemeOptions>(
                        default,
                        options => options.ForwardDefaultSelector = FirstAuthenticationThatHandles
                    );

                    services.AddOptions<AuthenticationSchemeOptions>();
                }
            })
            .Build();
    }

    string? FirstAuthenticationThatHandles(HttpContext context) =>
        _authentications.FirstOrDefault(a => a.Handles(context))?.Scheme;

    protected override PhaseContext GetContext(Build phase)
    {
        var app = Context.GetWebApplication();

        return phase.CreateContextBuilder()
            .Add(_middlewares)
            .Add<IEndpointRouteBuilder>(app)
            .OnDispose(() =>
            {
                if (_authentications.Any())
                {
                    app.UseAuthentication();
                }

                foreach (var middleware in _middlewares.OrderBy(m => m.Order))
                {
                    middleware.Configure(app);
                }
            })
            .Build();
    }

    protected override IEnumerable<IPhase> GetPhases()
    {
        yield return new CreateBuilder();
        yield return new Build();
        yield return new Run();
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
            Context.Add(app.Services);
        }
    }

    class Run()
        : PhaseBase<WebApplication>(PhaseOrder.Latest)
    {
        protected override void Initialize(WebApplication app)
        {
            app.Run();
        }
    }
}