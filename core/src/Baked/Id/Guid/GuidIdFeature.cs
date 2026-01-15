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
                    Type = c.Domain.Types[typeof(Orm.Id)].CSharpFriendlyFullName,
                    Key = "Id"
                }
            );
        });

        configurator.ConfigureAutomapping(automapping =>
        {
            automapping.MemberIsId.Add(m => m.PropertyType == typeof(Orm.Id) && m.Name == "Id");
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.CustomType<GuidIdUserType>()));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.GeneratedBy.Custom<GuidIdGenerator>()));
            model.Conventions.Add(ConventionBuilder.Id.Always(x => x.Unique()));
            model.Conventions.Add(ForeignKey.EndsWith("Id"));
        });
    }
}