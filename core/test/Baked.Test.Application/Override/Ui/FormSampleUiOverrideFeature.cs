using Baked.Architecture;
using Baked.RestApi;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using B = Baked.Ui.Components;

namespace Baked.Test.Override.Ui;

public class FormSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // contents
            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c => c.Type.Is<FormSample>(),
                component: dp =>
                {
                    dp.Schema.Content.ReloadOn(nameof(FormSample.ClearParents).Kebaberize());
                    dp.Schema.Content.ReloadOn(nameof(Parent.Delete).Kebaberize());
                    dp.Schema.Content.ReloadOn(nameof(Parent.Update).Kebaberize());
                }
            );

            // actions
            builder.Conventions.RemoveMethodAttribute<ActionAttribute>(
                when: c => c.Type.Is<Parent>() && c.Method.Name.Contains("Child"),
                order: RestApiLayer.MaxConventionOrder + 15
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                where: cc => cc.Path.StartsWith(nameof(Page), "*", "*", nameof(FormPage)),
                schema: ra => ra.PostAction = Actions.Local.UseRedirect("/form-sample")
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (c, cc) => B.DataTableColumn("Actions")
            );
        });
    }
}