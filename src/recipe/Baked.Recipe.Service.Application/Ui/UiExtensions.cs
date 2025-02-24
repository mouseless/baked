using Baked.Architecture;
using Baked.Ui;

namespace Baked;

public static class UiExtensions
{
    public static void AddUi(this List<ILayer> layers) =>
        layers.Add(new UiLayer());

    public static void ConfigurePageDescriptors(this LayerConfigurator configurator, Action<PageDescriptors> configure) =>
        configurator.Configure(configure);
}