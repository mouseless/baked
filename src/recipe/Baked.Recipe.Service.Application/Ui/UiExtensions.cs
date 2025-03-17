using Baked.Architecture;
using Baked.Domain.Model;
using Baked.Ui;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class UiExtensions
{
    public static void AddUi(this List<ILayer> layers) =>
        layers.Add(new UiLayer());

    public static void ConfigureAppDescriptor(this LayerConfigurator configurator, Action<AppDescriptor> configure) =>
        configurator.Configure(configure);

    public static void ConfigureLayoutDescriptors(this LayerConfigurator configurator, Action<LayoutDescriptors> configure) =>
        configurator.Configure(configure);

    public static void ConfigurePageDescriptors(this LayerConfigurator configurator, Action<PageDescriptors> configure) =>
        configurator.Configure(configure);

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
}