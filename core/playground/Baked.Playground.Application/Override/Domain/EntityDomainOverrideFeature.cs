using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.Orm;
using Baked.Ui;

namespace Baked.Playground.Override.Domain;

public class EntityDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddLocateAction<Entity>();

            conventions.SetPropertyAttribute(
                when: c => c.Type.Is<Entity>() && c.Property.Name == nameof(Entity.Guid),
                attribute: () => new LabelAttribute()
            );

            conventions.RemoveParameterSchema<Input>(
                when: c => c.Type.Is<Entity>() && c.Parameter.ParameterType.SkipNullable().Is<Guid>(),
                order: Order.At.Override
            );

            conventions.RemoveParameterSchema<Input>(
                when: c => c.Type.Is<Entity>() && c.Parameter.ParameterType.SkipNullable().Is<object>(),
                order: Order.At.Override
            );
        });
    }
}