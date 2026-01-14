using Baked.Architecture;
using Baked.Business;
using FluentNHibernate.Conventions.Helpers;

namespace Baked.Id.Guid;

public class GuidIdFeature : IFeature<IdConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetPropertyAttribute(
                when: c => c.Property.Name == "Id",
                attribute: c => new IdAttribute()
                {
                    Type = "Guid",
                    Key = "Id"
                }
            );
        });

        configurator.ConfigureAutomapping(automapping =>
        {
            automapping.MemberIsId.Add(m => m.PropertyType == typeof(System.Guid) && m.Name == "Id");
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.GeneratedBy.Guid()));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.Unique()));
            model.Conventions.Add(ForeignKey.EndsWith("Id"));
        });
    }
}