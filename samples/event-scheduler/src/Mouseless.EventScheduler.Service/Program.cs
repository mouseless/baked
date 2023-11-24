Forge.New
    .Service(
        database: c => c.Sqlite("EventScheduler.db"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
