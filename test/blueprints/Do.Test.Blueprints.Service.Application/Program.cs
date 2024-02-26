using Do.Test;
using Do.Test.RestApi.Analyzer;

Forge.New
    .Service(
        authentication: c => c.FixedToken(["Backend", "External"]),
        business: c => c.Default(assemblies: [typeof(Entity).Assembly], controllerAssembly: typeof(ParentsController).Assembly),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
