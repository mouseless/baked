using Do.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Do.Greeting.WelcomePage;

public class WelcomePageGreetingFeature(string _path)
    : IFeature<GreetingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add(app => app.UseWelcomePage(_path));
        });
    }
}