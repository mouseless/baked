using Do.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Do.Greeting.WelcomePage;

public class WelcomePageGreetingFeature : IFeature
{
    string _path;

    public WelcomePageGreetingFeature(string path) =>
        _path = path;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseWelcomePage(_path));
        });
    }
}
