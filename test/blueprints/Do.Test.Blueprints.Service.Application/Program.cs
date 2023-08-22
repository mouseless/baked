Forge.New
    .Service(
        business: c => c.Default(),
        database: c => (Do.Database.IDatabaseFeature)c.MySql().ForDevelopment(c.Sqlite()),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
