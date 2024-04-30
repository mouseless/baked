using Do.Test.Orm;

Forge.New
    .Service(
        business: c => c.DomainAssemblies([typeof(Entity).Assembly]),
        authentications: [
            c => c.FixedBearerToken(tokens =>
            {
                tokens.Add("Jane", claims: ["User"]);
                tokens.Add("John", claims: ["User", "Admin"]);
            }),
            c => c.ApiKey()
        ],
        authorization: c => c.ClaimBased(claims: ["User", "Admin"], baseClaim: "User"),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();