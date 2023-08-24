using Do.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Do.Greeting.WelcomePage;

public class WelcomePageGreetingFeature : IGreetingFeature
{
    readonly string _path;

    public WelcomePageGreetingFeature(string path) =>
        _path = path;

    public string Id => GetType().Name;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseWelcomePage(_path));
        });
    }
}
