using Do.Architecture;
using Do.Blueprints.Service.Greeting;

namespace Do;

public static class ForgeExtensions
{
    public static Application Service(this Forge source,
        Func<GreetingConfigurator, IFeature>? greeting = default,
        Action<ApplicationDescriptor>? configure = default
    )
    {
        greeting ??= c => c.HelloWorld();
        configure ??= _ => { };

        return source.Application(app =>
            {
                app.Layers.AddDependencyInjection();
                app.Layers.AddWeb();

                app.Features.AddGreeting(greeting);

                configure(app);
            });
    }
}
