using Baked.Architecture;
using Baked.Ui;
using Baked.Ui.Configuration;

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

    public static void ReloadOn(this ISupportsReaction source, string @event,
        IConstraint? constraint = default
    ) => source.AddReaction("reload", new OnTrigger(@event) { Constraint = constraint });

    public static void ReloadWhen(this ISupportsReaction source, string key,
        IConstraint? constraint = default
    ) => source.AddReaction("reload", new WhenTrigger(key) { Constraint = constraint });

    public static void ShowOn(this ISupportsReaction source, string @event,
        IConstraint? constraint = default
    ) => source.AddReaction("show", new OnTrigger(@event) { Constraint = constraint });

    public static void ShowWhen(this ISupportsReaction source, string key,
        IConstraint? constraint = default
    ) => source.AddReaction("show", new WhenTrigger(key) { Constraint = constraint });

    public static void AddReaction(this ISupportsReaction source, string reaction, ITrigger trigger)
    {
        source.Reactions ??= new();

        source.Reactions.TryGetValue(reaction, out var current);
        source.Reactions[reaction] = current + trigger;
    }
}