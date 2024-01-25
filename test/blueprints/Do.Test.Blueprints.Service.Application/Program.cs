using Do.Test;
using System.Reflection;

Forge.New
    .Service(
        business: c => c.Default(businessAssemblies: [typeof(Entity).Assembly], applicationParts: [Assembly.GetExecutingAssembly()]),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
