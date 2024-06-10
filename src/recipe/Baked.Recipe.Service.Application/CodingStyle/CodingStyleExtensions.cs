using Baked.Architecture;
using Baked.CodingStyle;

namespace Baked;

public static class CodingStyleExtensions
{
    public static void AddCodingStyles(this List<IFeature> source, IEnumerable<Func<CodingStyleConfigurator, IFeature<CodingStyleConfigurator>>> configures) =>
        source.AddRange(configures.Select(configure => configure(new())));
}