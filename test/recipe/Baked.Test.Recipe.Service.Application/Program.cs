using Baked.Test.Orm;
using System.Reflection;

Bake.New
    .Service(
        business: c => c.DomainAssemblies([typeof(Entity).Assembly], baseNamespace: "Baked.Test"),
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
        core: c => c.Dotnet(baseNamespace: _ => "Baked.Test")
            .ForNfr(c.Dotnet(entryAssembly: Assembly.GetExecutingAssembly(), baseNamespace: _ => "Baked.Test")),
        database: c => c
          .PostgreSql()
          .ForDevelopment(c.Sqlite())
          .ForNfr(c.Sqlite(fileName: $"Baked.Test.Recipe.Service.Nfr.db")),
        exceptionHandling: c => c.Default(typeUrlFormat: "https://baked.mouseless.codes/errors/{0}"),
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