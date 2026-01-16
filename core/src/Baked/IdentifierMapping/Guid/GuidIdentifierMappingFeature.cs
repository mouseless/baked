using Baked.Architecture;
using FluentNHibernate.Conventions.Helpers;

namespace Baked.IdentifierMapping.Guid;

public class GuidIdentifierMappingFeature : IFeature<IdentifierMappingConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.CustomType<GuidIdentifierUserType>()));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.GeneratedBy.Custom<GuidIdentifierGenerator>()));
        });
    }
}