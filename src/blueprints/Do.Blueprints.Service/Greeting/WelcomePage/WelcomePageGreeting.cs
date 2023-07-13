using Do.Architecture;
using Microsoft.AspNetCore.Builder;

namespace Do.Blueprints.Service.Greeting.WelcomePage;

public class WelcomePageGreeting : IFeature
{
    string _path;

    public WelcomePageGreeting(string path) => _path = path;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApplicationBuilder(app =>
        {
            app.UseWelcomePage(_path);
        });
    }
}
