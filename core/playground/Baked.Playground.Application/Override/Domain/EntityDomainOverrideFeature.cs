using Baked.Architecture;
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

            // Make Guid field an InputText
            conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.SkipNullable().Is<Guid>(),
                component: c => B.InputText()
            );

            // Make dynamic field an InputText
            conventions.AddParameterComponent(
                when: c => c.Parameter.Name == "dynamic",
                component: c => B.InputText()
            );
        });
    }
}