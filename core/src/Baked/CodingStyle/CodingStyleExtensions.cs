using Baked.Architecture;
using Baked.CodingStyle;

namespace Baked;

public static class CodingStyleExtensions
{
    public static void AddCodingStyles(this List<IFeature> features, IEnumerable<Func<CodingStyleConfigurator, IFeature<CodingStyleConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));
}