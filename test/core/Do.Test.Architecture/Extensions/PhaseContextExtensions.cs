using Do.Architecture;
using Do.Testing;

namespace Do.Test;

public static class PhaseContextExtensions
{
    public static PhaseContext APhaseContext(this Stubber giveMe,
        ApplicationContext? context = default,
        object? target = default,
        object[]? targets = default,
        Action? onDispose = default
    )
    {
        targets ??= [target ?? new()];
        onDispose ??= () => { };

        return new(targets.Select(t => giveMe.ALayerConfigurator(context: context, target: t)).ToList())
        {
            OnDispose = onDispose
        };
    }

    public static void ShouldConfigureTarget<TTarget>(this PhaseContext source, TTarget expected)
    {
        var configured = false;
        foreach (var configurator in source.Configurators)
        {
            configurator.Configure((TTarget actual) =>
            {
                actual.ShouldBe(expected);

                configured = true;
            });
        }

        configured.ShouldBeTrue("Phase context didn't get configured");
    }

    public static void ShouldConfigureTwoTargets<TTarget1, TTarget2>(this PhaseContext source, TTarget1 expected1, TTarget2 expected2)
    {
        var configured = false;
        foreach (var configurator in source.Configurators)
        {
            configurator.Configure((TTarget1 actual1, TTarget2 actual2) =>
            {
                actual1.ShouldBe(expected1);
                actual2.ShouldBe(expected2);

                configured = true;
            });
        }

        configured.ShouldBeTrue("Phase context didn't get configured");
    }

    public static void ShouldConfigureThreeTargets<TTarget1, TTarget2, TTarget3>(this PhaseContext source, TTarget1 expected1, TTarget2 expected2, TTarget3 expected3)
    {
        var configured = false;
        foreach (var configurator in source.Configurators)
        {
            configurator.Configure((TTarget1 actual1, TTarget2 actual2, TTarget3 actual3) =>
            {
                actual1.ShouldBe(expected1);
                actual2.ShouldBe(expected2);
                actual3.ShouldBe(expected3);

                configured = true;
            });
        }

        configured.ShouldBeTrue("Phase context didn't get configured");
    }

    public static void ShouldAddValueToContextOnDispose<T>(this PhaseContext source, T value, ApplicationContext context)
    {
        using (source)
        {
            context.ShouldNotHave(value);
        }

        context.ShouldHave(value);
    }
}