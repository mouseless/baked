using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

public static class PhaseContextExtensions
{
    extension(Stubber giveMe)
    {
        public PhaseContext APhaseContext(
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
    }

    extension(PhaseContext phaseContext)
    {
        public void ShouldConfigureTarget<TTarget>(TTarget expected)
        {
            var configured = false;
            foreach (var configurator in phaseContext.Configurators)
            {
                configurator.Configure((TTarget actual) =>
                {
                    actual.ShouldBe(expected);

                    configured = true;
                });
            }

            configured.ShouldBeTrue("Phase context didn't get configured");
        }

        public void ShouldConfigureTwoTargets<TTarget1, TTarget2>(TTarget1 expected1, TTarget2 expected2)
        {
            var configured = false;
            foreach (var configurator in phaseContext.Configurators)
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

        public void ShouldConfigureThreeTargets<TTarget1, TTarget2, TTarget3>(TTarget1 expected1, TTarget2 expected2, TTarget3 expected3)
        {
            var configured = false;
            foreach (var configurator in phaseContext.Configurators)
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

        public void ShouldAddValueToContextOnDispose<T>(T value, ApplicationContext context)
        {
            using (phaseContext)
            {
                context.ShouldNotHave(value);
            }

            context.ShouldHave(value);
        }
    }
}