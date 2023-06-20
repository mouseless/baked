var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDo(with => with.Service());

var app = builder.Build();
app.UseDo();
app.Run();

/**
Build.Application
    .As(app =>
    {
        app.Layers.AddDomain();
        app.Layers.AddMonitoring();
        app.Layers.AddRdbms();

        app.Features.AddAuthentication(c => c.JwtBearer());
        app.Features.AddLogging(c => c.Default());
        app.Features.AddDomainObjects(c => c.UseAssemblies("Do.Domain.Test*"));
    })
    .Run();

Build.Application
    .AsService(
        authentication: c => c.JwtBearer(),
        custom: app =>
        {
            app.Layers.AddCustom();

            app.Features.AddCustom();
        }
    )
    .Run();

Build.Application
    .AsGateway(
        gateway: c => c.Default(allow: "*", deny: "Admin.*")
    )
    .Run();
*/
