Build.Application
    .AsService(
        configure: app =>
        {
            app.Layers.AddDependencyInjection();
            app.Layers.AddWeb();

            app.Features.AddGreeting(c => c.HelloWorld());
        }
    )
    .Run();
