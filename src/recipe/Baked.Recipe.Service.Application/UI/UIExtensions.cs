using Baked.Architecture;
using Baked.UI;

namespace Baked;

public static class UIExtensions
{
    public static void AddUI(this List<ILayer> layers) =>
        layers.Add(new UILayer());

    public static void ConfigureComponentDescriptors(this LayerConfigurator configurator, Action<ComponentDescriptors> configure) =>
        configurator.Configure(configure);
}