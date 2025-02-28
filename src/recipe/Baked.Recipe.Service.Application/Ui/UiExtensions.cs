using Baked.Architecture;
using Baked.Ui;

namespace Baked;

public static class UiExtensions
{
    public static void AddUi(this List<ILayer> layers) =>
        layers.Add(new UiLayer());

    public static void ConfigureLayoutDescriptors(this LayerConfigurator configurator, Action<LayoutDescriptors> configure) =>
        configurator.Configure(configure);

    public static void ConfigurePageDescriptors(this LayerConfigurator configurator, Action<PageDescriptors> configure) =>
        configurator.Configure(configure);
}