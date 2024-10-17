using Baked.Test.Orm;

Bake.New
    .Service(
        business: c => c.DomainAssemblies([typeof(Entity).Assembly]),
        authentications: [
            c => c.FixedBearerToken(
                tokens =>
                {
                    tokens.Add("Jane", claims: ["User", "BaseA", "BaseB"]);
                    tokens.Add("John", claims: ["User", "Admin", "BaseA", "BaseB"]);
                    tokens.Add("Postman", claims: ["User", "Admin", "BaseA", "BaseB"]);

                    tokens.Add("Authenticated", claims: []);
                    tokens.Add("BaseClaims", claims: ["BaseA", "BaseB"]);
                    tokens.Add("GivenClaims", claims: ["GivenA", "GivenB"]);
                    tokens.Add("GivenAndBaseClaims", claims: ["GivenA", "GivenB", "BaseA", "BaseB"]);
                    tokens.Add("ClassClaims", claims: ["GivenA", "GivenB", "BaseA", "BaseB"]);
                    tokens.Add("MethodOverClassClaims", claims: ["GivenC"]);
                },
                formPostParameters: ["additional"],
                documentNames: ["samples"]
            ),
            c => c.ApiKey()
        ],
        authorization: c => c.ClaimBased(
            claims: ["User", "Admin", "BaseA", "BaseB", "GivenA", "GivenB", "GivenC"],
            baseClaims: ["BaseA", "BaseB"]
        ),
        database: c => c
          .PostgreSql()
          .ForDevelopment(c.Sqlite())
          .ForNfr(c.Sqlite(fileName: $"Baked.Test.Recipe.Service.Nfr.db")),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://baked.mouseless.codes/errors/{0}"),
        configure: app =>
        {
            app.Features.AddResource([
                    c => c.EmbeddedResource([
                        new(typeof(Entity), typeof(Entity).Assembly, string.Empty)
                    ]),
                c => c.Physical([
                    new(typeof(Entity), Path.GetDirectoryName(typeof(Entity).Assembly.Location))
                ])
                ]);
            app.Features.AddConfigurationOverrider();
        }
    )
    .Run();