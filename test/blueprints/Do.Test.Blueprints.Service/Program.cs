Build.Application
    .AsService(
        configure: app =>
        {
            app.Features.AddGreeting(c => c.HelloWorld());
        }
    )
    .Run();
