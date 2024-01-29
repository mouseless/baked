Forge.New
    .Service(
        business: c => c.Default(assemblies: [typeof(Contact).Assembly]),
        database: c => c.Sqlite("EventScheduler.db")
    )
    .Run();
