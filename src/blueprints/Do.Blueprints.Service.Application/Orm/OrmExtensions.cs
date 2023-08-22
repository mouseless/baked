using Do.Architecture;
using Do.Orm;

namespace Do;

public static class OrmExtensions
{
    public static void AddOrm(this List<IFeature> source, Func<OrmConfigurator, IOrmFeature> configure) => source.Add(configure(new()));
}
