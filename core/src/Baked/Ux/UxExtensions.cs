using Baked.Architecture;
using Baked.Ux;

namespace Baked;

public static class UxExtensions
{
    extension(List<IFeature> features)
    {
        public void AddUx(IEnumerable<Func<UxConfigurator, IFeature<UxConfigurator>>> configures) =>
            features.AddRange(configures.Select(configure => configure(new())));

        public void AddUxes(IEnumerable<Func<UxConfigurator, IFeature<UxConfigurator>>> configures) =>
            features.AddUx(configures);
    }
}