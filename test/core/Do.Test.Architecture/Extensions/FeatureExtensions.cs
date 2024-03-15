using Do.Architecture;
using Do.Testing;

namespace Do.Test;

public static class FeatureExtensions
{
    public static IFeature AFeature(this Mocker _,
        string? id = default
    )
    {
        var result = new Mock<IFeature>();
        result.Setup(l => l.Id).Returns(id ?? $"{Guid.NewGuid()}");

        return result.Object;
    }

    public static void VerifyInitialized(this IFeature source) =>
        Mock.Get(source).Verify(f => f.Configure(It.IsAny<LayerConfigurator>()));

    public static void VerifyConfigures<TTarget>(this IFeature source, TTarget target) where TTarget : notnull =>
        Mock.Get(source).Verify(f => f.Configure(LayerConfigurator.Create(new(), target)));

    public static void VerifyConfiguresNothing(this IFeature source) =>
        Mock.Get(source).Verify(f => f.Configure(It.IsAny<LayerConfigurator>()), Times.Never());
}
