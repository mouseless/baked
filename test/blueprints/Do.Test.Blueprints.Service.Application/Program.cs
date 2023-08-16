Forge.New
    .Service(
            business: c => c.Default(),
            database: c => c.MySql().ForDevelopment(c.Sqlite()),
            configure: app => app.Features.AddConfigurationOverrider()
        )
    .Run();
