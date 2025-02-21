using Baked.Architecture;
using Baked.Ui;

namespace Baked;

public static class UIExtensions
{
    public static void AddUi(this List<ILayer> layers) =>
        layers.Add(new UiLayer());

    public static void ConfigureComponentDescriptors(this LayerConfigurator configurator, Action<ComponentDescriptors> configure) =>
        configurator.Configure(configure);
}