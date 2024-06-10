using Do.Architecture;
using Do.CodingStyle;

namespace Do;

public static class CodingStyleExtensions
{
    public static void AddCodingStyles(this List<IFeature> source, IEnumerable<Func<CodingStyleConfigurator, IFeature<CodingStyleConfigurator>>> configures) =>
        source.AddRange(configures.Select(configure => configure(new())));
}