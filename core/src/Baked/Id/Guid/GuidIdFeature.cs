using Baked.Architecture;
using FluentNHibernate.Conventions.Helpers;

namespace Baked.Id.Guid;

public class GuidIdFeature : IFeature<IdConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.CustomType<GuidIdUserType>()));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.GeneratedBy.Custom<GuidIdGenerator>()));
        });
    }
}