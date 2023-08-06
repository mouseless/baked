Forge.New
    .Service(
        business: c => c.Default(),
        database: c => c.MySql()
                        .ForDevelopment(c.Sqlite())
    )
    .Run();
