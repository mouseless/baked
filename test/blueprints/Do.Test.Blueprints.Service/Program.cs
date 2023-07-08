Forge.New
    .Service(
        configure: app =>
        {
            app.Layers.AddDependencyInjection();
            app.Layers.AddWeb();

            app.Features.AddGreeting(c => c.WelcomePage());
        }
    )
    .Run();
