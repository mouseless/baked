using Baked.Architecture;

namespace Baked.Test.Override.Ui;

public class PluginSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAppDescriptor(app =>
        {
            app.Plugins.Add(new SamplePlugin());
        });
    }
}