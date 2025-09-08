using Baked.Architecture;
using Baked.Ui;

using static Baked.Ui.UiLayer;

namespace Baked;

public static class UiExtensions
{
    public static void AddUi(this List<ILayer> layers) =>
        layers.Add(new UiLayer());

    public static void ConfigureAppDescriptor(this LayerConfigurator configurator, Action<AppDescriptor> configure) =>
        configurator.Configure(configure);

    public static void ConfigureComponentExports(this LayerConfigurator configurator, Action<ComponentExports> configure) =>
        configurator.Configure(configure);

    public static void ConfigureLayoutDescriptors(this LayerConfigurator configurator, Action<LayoutDescriptors> configure) =>
        configurator.Configure(configure);

    public static void ConfigurePageDescriptors(this LayerConfigurator configurator, Action<PageDescriptors> configure) =>
        configurator.Configure(configure);

    public static void UsingLocaleTemplate(this LayerConfigurator configurator, Action<ILocaleTemplate> localeTemplate) =>
       configurator.Use(localeTemplate);

    public static void UsingLocalization(this LayerConfigurator configurator, Action<NewLocaleKey> l) =>
        configurator.Use(l);

    public static void AddFromExtensions(this ComponentExports exports, Type type)
    {
        var extensions = type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public) ?? [];
        var componentTypes = extensions
            .Where(m =>
                m.ReturnType.IsAssignableTo(typeof(IComponentDescriptor)) &&
                !m.GetGenericArguments().Any()
            )
            .Select(m => m.Name);

        exports.AddRange(componentTypes);
    }
}