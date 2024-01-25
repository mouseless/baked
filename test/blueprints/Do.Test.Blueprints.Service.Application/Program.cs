using Do.Test;

Forge.New
    .Service(
        business: c => c.Default(options =>
        {
            options.AddBusinessAssembly<Entity>();
        }),
        database: c => c.MySql().ForDevelopment(c.Sqlite()),
        exceptionHandling: ex => ex.Default(typeUrlFormat: "https://do.mouseless.codes/errors/{0}"),
        configure: app => app.Features.AddConfigurationOverrider()
    )
    .Run();
