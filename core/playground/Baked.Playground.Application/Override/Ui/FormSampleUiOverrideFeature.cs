using Baked.Architecture;
using Baked.Playground.Orm;
using Baked.Playground.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

namespace Baked.Playground.Override.Ui;

public class FormSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddMethodAttributeConfiguration<ActionAttribute>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                attribute: (a, c) => a.RoutePathBack = "/form-sample"
            );
            builder.Conventions.AddMethodAttributeConfiguration<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name.Contains("Child"),
                attribute: a => a.HideInLists = true
            );

            builder.Conventions.AddMethodComponentConfiguration<DataTable>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                component: dt => dt.ReloadOn(nameof(FormSample.ClearParents).Kebaberize())
            );
        });
    }
}