using Baked.Test.Orm;

Bake.New
    .Service(
        business: c => c.DomainAssemblies(typeof(Entity).Assembly,
            baseNamespace: "Baked.Test",
            setNamespaceWhen: t => t.Namespace is not null && t.Namespace.StartsWith("Baked.Test.CodingStyle.NamespaceAsRoute")
        ),
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
        core: c => c.Dotnet(baseNamespace: "Baked.Test"),
        cors: c => c.AspNetCore(Settings.Required<string>("CorsOrigin")),
        database: c => c
            .Sqlite()
            .ForProduction(c.PostgreSql()),
        exceptionHandling: c => c.ProblemDetails(typeUrlFormat: "https://baked.mouseless.codes/errors/{0}"),
        configure: app =>
        {
            app.Features.AddReporting(c => c
                .NativeSql(basePath: "Reporting/Sqlite")
                .ForProduction(c.NativeSql(basePath: "Reporting/PostgreSql"))
            );
            app.Features.AddConfigurationOverrider();
        }
    )
    .Run();