Bake.New
    .Monolith(
        business: c => c.DomainAssemblies(typeof(Contact).Assembly),
        options: mr =>
        {
            mr.Database(c => c.Sqlite("Mouseless.EventScheduler.db"));
            mr.Configure(app => app.Features.AddOverrides());
        }
    )
    .Run();