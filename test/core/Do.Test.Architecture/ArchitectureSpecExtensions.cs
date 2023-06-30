using Do.Architecture;
using Do.Branding;

namespace Do.Test;

public static class ArchitectureSpecExtensions
{
    #region Build

    public static Build ABuild(this Spec.Stubber source,
        IBanner? banner = default
    )
    {
        banner ??= source.Spec.MockMe.ABanner();

        return new(banner);
    }

    #endregion

    #region Application

    public static IRunnable AnApplication(this Spec.Stubber source,
        ILayer? layer = default,
        ILayer[]? layers = default,
        IFeature? feature = default,
        IFeature[]? features = default
    )
    {
        layers ??= new[] { layer ?? source.Spec.MockMe.ALayer() };
        features ??= new[] { feature ?? source.Spec.MockMe.AFeature() };

        return source.ABuild().As(app =>
        {
            app.Layers.AddRange(layers);
            app.Features.AddRange(features);
        });
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
                .Setup(l => l.GetConfigurationTarget(It.IsAny<IPhase>(), It.IsAny<ApplicationContext>()))
                .Returns(ConfigurationTarget.Create(configurationTarget));
        }

        if (onApplyPhase != default)
        {
            result
                .Setup(l => l.GetConfigurationTarget(It.IsAny<IPhase>(), It.IsAny<ApplicationContext>()))
                .Returns(ConfigurationTarget.Empty)
                .Callback((IPhase _, ApplicationContext _) => onApplyPhase());
        }

        return result.Object;
    }

    public static void VerifyInitialized(this ILayer source) =>
        Mock.Get(source).Verify(l => l.GetPhases());

    public static void VerifyApplied(this ILayer source, IPhase phase) =>
        Mock.Get(source)
            .Verify(l => l.GetConfigurationTarget(
                phase,
                It.IsAny<ApplicationContext>()
            ));

    #endregion

    #region Phase

    public static IPhase APhase(this Spec.Mocker source,
        Action? onInitialize = default
    )
    {
        var result = new Mock<IPhase>();

        if (onInitialize != default)
        {
            result
                .Setup(p => p.Initialize(It.IsAny<ApplicationContext>()))
                .Callback((ApplicationContext _) => onInitialize());
        }

        return result.Object;
    }

    public static void VerifyInitialized(this IPhase source) =>
        Mock.Get(source).Verify(p => p.Initialize(It.IsAny<ApplicationContext>()));

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
