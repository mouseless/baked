using Baked.Architecture;
using Baked.Domain.Model;
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
        public void AddOrm(FeatureFunc<OrmConfigurator> configure) =>
            features.Add(configure(new()));
    }

    extension(TypeModelMembers members)
    {
        public IEnumerable<PropertyModel> GetEntityReferenceProperties() =>
            members.Properties.Where(p =>
                p.IsAutoProperty &&
                p.PropertyType.TryGetMetadata(out var metadata) &&
                metadata.Has<EntityAttribute>()
            );
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