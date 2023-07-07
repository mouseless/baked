using Do.Architecture;
using Do.Branding;

namespace Do.Test;

public static class ArchitectureSpecExtensions
{
    #region Object

    public static T An<T>(this Spec.Stubber source) => source.A<T>();
    public static T A<T>(this Spec.Stubber source)
    {
        var result = Activator.CreateInstance(typeof(T));

        Assume.That(result, Is.Not.Null);

        return (T)result!;
    }

    #endregion

    #region Build

    public static Build ABuild(this Spec.Stubber source,
        IBanner? banner = default,
        ApplicationContext? context = default
    )
    {
        banner ??= source.Spec.MockMe.ABanner();
        context ??= new();

        return new(banner, () => new(context));
    }

    #endregion

    #region Application

    public static Application AnApplication(this Spec.Stubber source,
        ILayer? layer = default,
        ILayer[]? layers = default,
        IFeature? feature = default,
        IFeature[]? features = default,
        ApplicationContext? context = default
    )
    {
        layers ??= new[] { layer ?? source.Spec.MockMe.ALayer() };
        features ??= new[] { feature ?? source.Spec.MockMe.AFeature() };

        return source.ABuild(context: context).As(app =>
        {
            app.Layers.AddRange(layers);
            app.Features.AddRange(features);
        });
    }

    #endregion

    #region ApplicationContext

    public static ApplicationContext AnApplicationContext(this Spec.Stubber source) => new();
    public static ApplicationContext AnApplicationContext<T>(this Spec.Stubber source, T content) where T : notnull
    {
        var result = source.AnApplicationContext();

        result.Add(content);

        return result;
    }

    public static void ShouldHave<T>(this ApplicationContext context, T value)
    {
        Assert.That(context.Has<T>(), Is.True, $"Context should have an item with type {typeof(T)}");
        Assert.That(context.Get<T>(), Is.EqualTo(value));
    }

    public static void ShouldNotHave<T>(this ApplicationContext context, T value)
    {
        if (context.Has<T>())
        {
            Assert.That(context.Get<T>(), Is.Not.EqualTo(value));
        }
    }

    #endregion

    #region Banner

    public static IBanner ABanner(this Spec.Mocker source) =>
        new Mock<IBanner>().Object;

    public static void VerifyPrinted(this IBanner source) =>
        Mock.Get(source).Verify(b => b.Print());

    #endregion

    #region Layer

    public static ILayer ALayer(this Spec.Mocker source,
        object? configurationTarget = default,
        IPhase? phase = default,
        IPhase[]? phases = default,
        Action? onApplyPhase = default
    )
    {
        phases ??= new[] { phase ?? source.APhase() };

        var result = new Mock<ILayer>();

        result.Setup(l => l.GetPhases()).Returns(phases);

        if (configurationTarget != default)
        {
            result
                .Setup(l => l.GetContext(It.IsAny<IPhase>(), It.IsAny<ApplicationContext>()))
                .Returns(new PhaseContextBuilder().Add(configurationTarget).Build());
        }

        if (onApplyPhase != default)
        {
            result
                .Setup(l => l.GetContext(It.IsAny<IPhase>(), It.IsAny<ApplicationContext>()))
                .Returns(PhaseContext.Empty)
                .Callback((IPhase _, ApplicationContext _) => onApplyPhase());
        }

        return result.Object;
    }

    public static void VerifyInitialized(this ILayer source) =>
        Mock.Get(source).Verify(l => l.GetPhases());

    public static void VerifyApplied(this ILayer source, IPhase phase) =>
        Mock.Get(source)
            .Verify(l => l.GetContext(
                phase,
                It.IsAny<ApplicationContext>()
            ));

    #endregion

    #region Phase

    public static IPhase APhase(this Spec.Mocker source,
        Func<bool>? isReady = default,
        Action? onInitialize = default,
        PhaseOrder order = PhaseOrder.Normal
    )
    {
        isReady ??= () => true;
        onInitialize ??= () => { };

        var result = new Mock<IPhase>();

        result.Setup(p => p.Order).Returns(order);

        result
            .Setup(p => p.IsReady(It.IsAny<ApplicationContext>()))
            .Returns((ApplicationContext _) => isReady());

        result
            .Setup(p => p.Initialize(It.IsAny<ApplicationContext>()))
            .Callback((ApplicationContext _) => onInitialize());

        return result.Object;
    }

    public static void VerifyInitialized(this IPhase source) =>
        Mock.Get(source).Verify(p => p.Initialize(It.IsAny<ApplicationContext>()));

    public static void VerifyInitialized(this IPhase source, ApplicationContext context) =>
        Mock.Get(source).Verify(p => p.Initialize(context));

    #endregion

    #region PhaseContext

    public static void ShouldConfigureTarget<TTarget>(this PhaseContext source, TTarget expected)
    {
        var configured = false;
        foreach (var configurator in source.Configurators)
        {
            configurator.Configure((TTarget actual) =>
            {
                Assert.That(actual, Is.EqualTo(expected));

                configured = true;
            });
        }

        Assert.That(configured, Is.True, "Phase context didn't get configured");
    }

    public static void ShouldConfigureTwoTargets<TTarget1, TTarget2>(this PhaseContext source, TTarget1 expected1, TTarget2 expected2)
    {
        var configured = false;
        foreach (var configurator in source.Configurators)
        {
            configurator.Configure((TTarget1 actual1, TTarget2 actual2) =>
            {
                Assert.That(actual1, Is.EqualTo(expected1));
                Assert.That(actual2, Is.EqualTo(expected2));

                configured = true;
            });
        }

        Assert.That(configured, Is.True, "Phase context didn't get configured");
    }

    public static void ShouldConfigureThreeTargets<TTarget1, TTarget2, TTarget3>(this PhaseContext source, TTarget1 expected1, TTarget2 expected2, TTarget3 expected3)
    {
        var configured = false;
        foreach (var configurator in source.Configurators)
        {
            configurator.Configure((TTarget1 actual1, TTarget2 actual2, TTarget3 actual3) =>
            {
                Assert.That(actual1, Is.EqualTo(expected1));
                Assert.That(actual2, Is.EqualTo(expected2));
                Assert.That(actual3, Is.EqualTo(expected3));

                configured = true;
            });
        }

        Assert.That(configured, Is.True, "Phase context didn't get configured");
    }

    public static void ShouldAddValueToContextOnDispose<T>(this PhaseContext source, T value, ApplicationContext context)
    {
        using (source)
        {
            context.ShouldNotHave(value);
        }

        context.ShouldHave(value);
    }

    #endregion

    #region Feature

    public static IFeature AFeature(this Spec.Mocker source) =>
        new Mock<IFeature>().Object;

    public static void VerifyInitialized(this IFeature source) =>
        Mock.Get(source).Verify(f => f.Configure(It.IsAny<ConfigurationTarget>()));

    public static void VerifyConfigures(this IFeature source, object configurationTarget) =>
        Mock.Get(source).Verify(f => f.Configure(ConfigurationTarget.Create(configurationTarget)));

    #endregion
}
