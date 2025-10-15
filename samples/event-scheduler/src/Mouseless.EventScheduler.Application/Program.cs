Bake.New
    .Monolith(
        business: c => c.DomainAssemblies(typeof(Contact).Assembly),
        database: c => c.Sqlite("Mouseless.EventScheduler.Application.db"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
