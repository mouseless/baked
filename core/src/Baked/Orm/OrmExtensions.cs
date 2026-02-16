using Baked.Architecture;
using Baked.Orm;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Shouldly;

namespace Baked;

public static class OrmExtensions
{
    public static void AddOrm(this List<IFeature> features, Func<OrmConfigurator, IFeature<OrmConfigurator>> configure) =>
        features.Add(configure(new()));

    public static void ShouldBeDeleted(this object @object) =>
        Spec
          .StartContextStatic
          .GetServiceProvider()
          .UsingCurrentScope()
          .GetRequiredService<ISession>()
          .Contains(@object)
          .ShouldBeFalse($"{@object} should've been deleted, but it's STILL in the session");

    public static void ShouldBeInserted(this object @object) =>
        Spec
          .StartContextStatic
          .GetServiceProvider()
          .UsingCurrentScope()
          .GetRequiredService<ISession>()
          .Contains(@object)
          .ShouldBeTrue($"{@object} should've been inserted, but it's NOT in the session");
}