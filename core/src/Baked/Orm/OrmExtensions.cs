using Baked.Architecture;
using Baked.Orm;
using Baked.Testing;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Shouldly;

namespace Baked;

public static class OrmExtensions
{
    extension(List<IFeature> features)
    {
        public void AddOrm(Func<OrmConfigurator, IFeature<OrmConfigurator>> configure) =>
            features.Add(configure(new()));
    }

    extension(object @object)
    {
        public void ShouldBeDeleted() =>
            Spec
            .StartContextStatic
            .GetServiceProvider()
            .UsingCurrentScope()
            .GetRequiredService<ISession>()
            .Contains(@object)
            .ShouldBeFalse($"{@object} should've been deleted, but it's STILL in the session");

        public void ShouldBeInserted() =>
            Spec
              .StartContextStatic
              .GetServiceProvider()
              .UsingCurrentScope()
              .GetRequiredService<ISession>()
              .Contains(@object)
              .ShouldBeTrue($"{@object} should've been inserted, but it's NOT in the session");
    }
}