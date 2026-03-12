using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

public static class FeatureExtensions
{
    extension(Mocker _)
    {
        public IFeature AFeature(
            string? id = default
        )
        {
            var result = new Mock<IFeature>();
            result.Setup(l => l.Id).Returns(id ?? $"{Guid.NewGuid()}");

            return result.Object;
        }
    }

    extension(IFeature feature)
    {
        public void VerifyInitialized() =>
            Mock.Get(feature).Verify(f => f.Configure(It.IsAny<LayerConfigurator>()));

        public void VerifyConfigures<TTarget>(TTarget target) where TTarget : notnull =>
            Mock.Get(feature).Verify(f => f.Configure(LayerConfigurator.Create(new(), target)));

        public void VerifyConfiguresNothing() =>
            Mock.Get(feature).Verify(f => f.Configure(It.IsAny<LayerConfigurator>()), Times.Never());
    }
}