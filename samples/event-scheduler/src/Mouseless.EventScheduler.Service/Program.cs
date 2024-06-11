Bake.New
    .Service(
        business: c => c.DomainAssemblies([typeof(Contact).Assembly]),
        database: c => c.Sqlite("EventScheduler.db")
    )
    .Run();
