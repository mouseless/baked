using Do.Architecture;
using Do.Core;

namespace Do;

public static class CoreExtensions
{
    public static void AddCore(this List<IFeature> source, Func<CoreConfigurator, IFeature> configure) => source.Add(configure(new()));
}