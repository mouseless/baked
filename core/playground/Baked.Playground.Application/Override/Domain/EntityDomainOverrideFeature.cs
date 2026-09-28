using Baked.Architecture;
using Baked.Business;
using Baked.Playground.Orm;

using B = Baked.Ui.Components;

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

            // Make Guid field an InputText
            conventions.AddParameterComponent(
                when: c => c.Type.Is<Entity>() && c.Parameter.ParameterType.SkipNullable().Is<Guid>(),
                component: c => B.InputText()
            );

            // Make dynamic field an InputText
            conventions.AddParameterComponent(
                when: c => c.Type.Is<Entity>() && c.Parameter.Name == "dynamic",
                component: c => B.InputText()
            );
        });
    }
}