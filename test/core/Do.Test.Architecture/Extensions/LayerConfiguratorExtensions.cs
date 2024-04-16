using Do.Architecture;
using Do.Testing;
using System.Reflection;

namespace Do.Test;

public static class LayerConfiguratorExtensions
{
    public static LayerConfigurator ALayerConfigurator<TTarget>(this Stubber giveMe,
        ApplicationContext? context = default,
        TTarget? target = default
    ) where TTarget : notnull
    {
        context ??= giveMe.AnApplicationContext();
        target ??= giveMe.AnInstanceOf<TTarget>();

        return LayerConfigurator.Create(context, target);
    }

    public static LayerConfigurator ALayerConfigurator<TTarget1, TTarget2>(this Stubber giveMe,
        ApplicationContext? context = default,
        TTarget1? target1 = default,
        TTarget2? target2 = default
    ) where TTarget1 : notnull
      where TTarget2 : notnull
    {
        context ??= giveMe.AnApplicationContext();
        target1 ??= giveMe.AnInstanceOf<TTarget1>();
        target2 ??= giveMe.AnInstanceOf<TTarget2>();

        return LayerConfigurator.Create(context, target1, target2);
    }

    public static LayerConfigurator ALayerConfigurator<TTarget1, TTarget2, TTarget3>(this Stubber giveMe,
        ApplicationContext? context = default,
        TTarget1? target1 = default,
        TTarget2? target2 = default,
        TTarget3? target3 = default
    ) where TTarget1 : notnull
      where TTarget2 : notnull
      where TTarget3 : notnull
    {
        context ??= giveMe.AnApplicationContext();
        target1 ??= giveMe.AnInstanceOf<TTarget1>();
        target2 ??= giveMe.AnInstanceOf<TTarget2>();
        target3 ??= giveMe.AnInstanceOf<TTarget3>();

        return LayerConfigurator.Create(context, target1, target2, target3);
    }

    public static LayerConfigurator ALayerConfigurator(this Stubber giveMe,
        ApplicationContext? context = default,
        object? target = default
    )
    {
        context ??= giveMe.AnApplicationContext();
        target ??= new();

        var create = typeof(LayerConfigurator)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(c => c.Name == nameof(LayerConfigurator.Create) && c.GetGenericArguments().Length == 1);
        create.ShouldNotBeNull();

        var configurator = create.MakeGenericMethod(target.GetType()).Invoke(null, [context, target]);
        configurator.ShouldNotBeNull();

        return (LayerConfigurator)configurator;
    }
}