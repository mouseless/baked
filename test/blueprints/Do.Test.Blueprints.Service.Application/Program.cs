using Do.Test.Orm;

Forge.New
    .Service(
        business: c => c.DomainAssemblies([typeof(Entity).Assembly]),
        authentications: [
            c => c.FixedBearerToken(tokens =>
            {
                tokens.Add("User", claims: ["User"]);
                tokens.Add("Admin", claims: ["Admin"]);
                tokens.Add("System", claims: ["System", "Admin"]);
            })
        ],
        authorization: c => c.ClaimBased(baseClaim: "User", claims: ["System", "Admin"]),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();