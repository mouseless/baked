Forge.New
    .Service(
        business: c => c.Default(),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        configure: app => app.Features.AddConfigurationOverrider(),
        exceptionHandling: e => e.Default(exceptionTypeUrl: "http://www.google.com/{0}")
    )
    .Run();
