using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.Orm;
using Baked.Theme.Default;

using Baked.Ui;

namespace Baked.Playground.Override.Domain;

public class EntityDomainOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddLocateAction<Entity>();
            conventions.AddEntityRemoteData<Entity>();

            conventions.SetPropertyAttribute(
                when: c => c.Type.Is<Entity>() && c.Property.Name is nameof(Entity.Unique),
                attribute: () => new LabelAttribute(),
                order: Order.At.Override
            );

            conventions.RemoveParameterSchema<Input>(
                when: c => c.Type.Is<Entity>() && c.Parameter.Name is "dynamic" or "guid" or "timeonly" or "dateTime",
                order: Order.At.Override
            );

            conventions.RemovePropertyAttribute<DataAttribute>(
                when: c => c.Type.Is<Entity>() && c.Property.Name is nameof(Entity.Dynamic) or nameof(Entity.Guid) or nameof(Entity.TimeOnly) or nameof(Entity.Enum),
                order: Order.At.Override
            );

            conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Entity>() && c.Method.Name is nameof(Entity.UpdateString) or nameof(Entity.LockAndIncrementInt32),
                order: Order.At.Theme.Override
            );
        });
    }
}