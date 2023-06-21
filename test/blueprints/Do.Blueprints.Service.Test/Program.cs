Build.Application
    .AsService()
    .Run();

/*

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
