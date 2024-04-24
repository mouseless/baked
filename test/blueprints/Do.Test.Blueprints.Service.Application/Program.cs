using Do.Test.Orm;

Forge.New
    .Service(
        business: c => c.DomainAssemblies([typeof(Entity).Assembly]),
        authorization: c => c.ClaimBased(policies:
            [
                new("Default", policy => policy.RequireClaim("Token")),
                new("AdminOnly", policy => policy.RequireClaim("Token", "788db39fd347455daf438c96d14c3ea2"))
            ]
        ),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();