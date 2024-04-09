using Do.Test.Orm;

Forge.New
    .Service(
        business: c => c.DomainAssemblies([typeof(Entity).Assembly]),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();