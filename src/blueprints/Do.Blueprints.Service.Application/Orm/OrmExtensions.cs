using Do.Architecture;
using Do.Orm;
using Shouldly;

namespace Do;

public static class OrmExtensions
{
    public static void AddOrm(this List<IFeature> source, Func<OrmConfigurator, IFeature<OrmConfigurator>> configure) => source.Add(configure(new()));

    public static void ShouldBeDeleted(this object @object) =>
        ServiceSpec.Session.Contains(@object).ShouldBeFalse($"{@object} should've been deleted, but it's STILL in the session");

    public static void ShouldBeInserted(this object @object) =>
        ServiceSpec.Session.Contains(@object).ShouldBeTrue($"{@object} should've been inserted, but it's NOT in the session");
}
