Forge.New
    .Service(
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
