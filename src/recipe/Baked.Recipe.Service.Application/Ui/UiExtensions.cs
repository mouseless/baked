using Baked.Architecture;
using Baked.Domain.Model;
using Baked.Ui;
using System.Diagnostics.CodeAnalysis;

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

    public static void UsingLocaleDictionary(this LayerConfigurator configurator, Action<ILocaleDictionary> locales) =>
       configurator.Use(locales);

    public static void UsingNewLocale(this LayerConfigurator configurator, Action<NewLocale> l) =>
        configurator.Use(l);

    public static bool TryGet<TSchema>(this TypeModel type, [NotNullWhen(true)] out TSchema? schema)
        where TSchema : IComponentSchema
    {
        schema = default;

        if (!type.TryGetMembers(out var members)) { return false; }
        if (!members.TryGetSingle<ComponentDescriptorAttribute<TSchema>>(out var descriptor)) { return false; }

        schema = descriptor.Schema;

        return true;
    }

    public static TSchema Get<TSchema>(this TypeModel type)
        where TSchema : IComponentSchema
    {
        if (!type.TryGet<TSchema>(out var result)) { throw new($"{type.Name} does not have ${typeof(TSchema).Name}"); }

        return result;
    }

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