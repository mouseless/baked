using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

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

    public static void VerifyInitialized(this IFeature feature) =>
        Mock.Get(feature).Verify(f => f.Configure(It.IsAny<LayerConfigurator>()));

    public static void VerifyConfigures<TTarget>(this IFeature feature, TTarget target) where TTarget : notnull =>
        Mock.Get(feature).Verify(f => f.Configure(LayerConfigurator.Create(new(), target)));

    public static void VerifyConfiguresNothing(this IFeature feature) =>
        Mock.Get(feature).Verify(f => f.Configure(It.IsAny<LayerConfigurator>()), Times.Never());
}