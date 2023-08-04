Forge.New
    .Service(
        business: c => c.Default(),
        database: c => c.Sqlite("Do.Test.Blueprints.Service.Application.db")
    )
    .Run();
