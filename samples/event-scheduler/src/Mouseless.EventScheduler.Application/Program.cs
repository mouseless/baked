Bake.New
    .Monolith(
        business: c => c.DomainAssemblies(typeof(Contact).Assembly),
        options: recipe =>
        {
            recipe.Database = c => c.Sqlite("Mouseless.EventScheduler.db");
            recipe.Configure = app => app.Features.AddOverrides();
        }
    )
    .Run();