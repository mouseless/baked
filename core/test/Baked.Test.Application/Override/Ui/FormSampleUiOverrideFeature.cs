using Baked.Architecture;
using Baked.RestApi;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;

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
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Delete),
                where: cc => cc.Path.EndsWith(nameof(DataTable.Actions), "*"),
                component: (c, cc) => MethodButton(c.Method, cc,
                    options: b =>
                    {
                        b.Icon = "pi pi-trash";
                        b.Label = string.Empty;
                        b.Variant = "text";
                        b.Rounded = true;
                        b.Severity = "danger";
                    }
                )
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Update),
                where: cc => cc.Path.EndsWith(nameof(SimpleForm), nameof(SimpleForm.DialogOptions), nameof(SimpleForm.Dialog.Open)),
                component: b =>
                {
                    b.Schema.Label = string.Empty;
                    b.Schema.Icon = "pi pi-pencil";
                    b.Schema.Variant = "text";
                    b.Schema.Rounded = true;
                }
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.Actions)),
                schema: (c, cc) => B.DataTableColumn("Actions")
            );
        });
    }
}