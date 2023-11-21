Forge.New
    .Service(
        business: c => c.Default(),
        database: c => c.Sqlite("EventScheduler.db"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
