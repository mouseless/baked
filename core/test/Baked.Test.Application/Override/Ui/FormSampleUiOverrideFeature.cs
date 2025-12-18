using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Test.Ui;
using Baked.Theme;
using Baked.Ui;
using Humanizer;

using static Baked.Test.Theme.Custom.DomainComponents;
using static Baked.Theme.Default.DomainActions;
using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

using B = Baked.Ui.Components;
using C = Baked.Test.Ui.Components;

namespace Baked.Test.Override.Ui;

public class FormSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // TODO - review this in form components
            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Is<FormSample>(),
                where: cc => cc.Path.Is(nameof(Page), "*"),
                component: (c, cc) => TypeSimplePage(c.Type, cc)
            );

            // contents
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<FormSample>(),
                component: (sp, c, cc) =>
                {
                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionModelAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method != HttpMethod.Get) { continue; }

                        var schema = method.GetSchema<SimplePage.Content>(
                            cc.Drill(nameof(SimplePage.Contents), sp.Schema.Contents.Count)
                        );
                        if (schema is null) { continue; }

                        sp.Schema.Contents.Add(schema);
                    }
                }
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Has<ActionModelAttribute>() && c.Method.Name.StartsWith("Get"),
                schema: (c, cc) => MethodSimplePageContent(c.Method, cc)
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<FormSample>() && c.Method.Has<ActionModelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(SimplePage.Contents), "*", "*", nameof(SimplePage.Content.Component)),
                component: (c, cc) => MethodDataPanel(c.Method, cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c => c.Type.Is<FormSample>(),
                component: dp =>
                {
                    dp.Schema.Content.ReloadOn(nameof(FormSample.ClearParents).Kebaberize());
                    dp.Schema.Content.ReloadOn(nameof(Parent.Delete).Kebaberize());
                }
            );

            // actions
            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Is<FormSample>(),
                where: cc => cc.Path.EndsWith(nameof(SimplePage.Title)),
                component: (c, cc) => TypePageTitle(c.Type, cc)
            );
            builder.Conventions.AddTypeComponentConfiguration<PageTitle>(
                when: c => c.Type.Is<FormSample>(),
                component: (pt, c, cc) =>
                {
                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionModelAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method == HttpMethod.Get) { continue; }

                        var actionComponent = method.GetComponent(cc.Drill(nameof(PageTitle.Actions), method.Name));
                        if (actionComponent is null) { continue; }

                        pt.Schema.Actions.Add(actionComponent);
                    }
                }
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<FormSample>() && c.Method.Has<ActionModelAttribute>() && !c.Method.Name.StartsWith("Get"),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions), "*"),
                component: (c, cc) => MethodButton(c.Method, cc)
            );

            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => true,
                schema: c => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => true,
                schema: ra => ra.Body = Context.Model()
            );
            builder.Conventions.AddMethodComponentConfiguration<SimpleForm>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimpleForm));

                    sf.Action = c.Method.GetRequiredSchema<RemoteAction>(cc.Drill(nameof(IComponentDescriptor.Action)));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        sf.Schema.Inputs.Add(
                            parameter.GetRequiredSchema<Input>(cc.Drill(nameof(SimpleForm.Inputs), parameter.Name))
                        );
                    }
                }
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Type.Is<FormSample>() && c.Parameter.ParameterType.Is<string>(),
                component: c => C.InputText(c.Parameter.Name)
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Type.Is<FormSample>() && c.Parameter.ParameterType.Is<int>(),
                component: c => C.InputNumber(c.Parameter.Name)
            );
            // END OF TODO - review this in form components
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.ClearParents),
                where: cc => true,
                schema: (c, _) => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                when: c => c.Type.Is<FormSample>(),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions), nameof(FormSample.NewParent)),
                component: b => b.Action = Actions.Local.UseRedirect("/form-sample/new-parent")
            );

            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(FormSample), nameof(FormSample.NewParent)),
                component: (c, cc) => MethodContainerPage(c.Method, cc.Drill(c.Method.Name))
            );
            builder.Conventions.AddMethodComponent(
                when: c => !c.Method.Name.StartsWith("Get") && c.Method.Has<ActionModelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(FormSample), nameof(FormSample.NewParent), nameof(ContainerPage), nameof(ContainerPage.Contents), "*"),
                component: (c, cc) =>
                {
                    var (_, l) = cc;

                    return B.SimpleForm(options: vf => vf.ButtonLabel = l(c.Method.Name.Titleize()));
                }
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                schema: ra => ra.PostAction = Actions.Local.UseRedirect("/form-sample")
            );

            // TODO row action
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Delete),
                schema: (c, cc) => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Delete),
                schema: ra => ra.Params = Context.Parent(options: o => o.Prop = "row")
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Delete),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate)),
                component: (c, cc) => MethodButton(c.Method, cc,
                    options: b =>
                    {
                        b.Icon = "pi pi-trash";
                        b.Label = string.Empty;
                        b.Variant = "text";
                        b.Rounded = true;
                    }
                )
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate)),
                schema: (c, cc) => B.DataTableColumn("Actions")
            );
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                schema: (col, c, cc) =>
                {
                    cc = cc.Drill(nameof(DataTable), nameof(DataTable.ActionTemplate));
                    if (!c.Method.DefaultOverload.ReturnType.TryGetGenerics(out var generics)) { return; }

                    var returnType = generics.GenericTypeArguments.First().Model;
                    var rowActions = new List<IComponentDescriptor>();
                    foreach (var method in returnType.GetMembers().Methods)
                    {
                        var component = method.GetComponent(cc);
                        if (component is null) { continue; }

                        rowActions.Add(component);
                    }

                    col.Frozen = true;
                    col.AlignRight = true;
                    col.Exportable = false;
                    // temporarily add first action
                    col.Component = rowActions.First();
                }
            );

            // TODO - move to default feature
            builder.Conventions.AddMethodComponentConfiguration<ContainerPage>(
                where: cc => cc.Path.EndsWith(nameof(FormSample), nameof(FormSample.NewParent)),
                component: (container, c, cc) =>
                {
                    cc = cc.Drill(nameof(ContainerPage), nameof(ContainerPage.Contents));

                    var component = c.Method.GetRequiredComponent(cc.Drill(container.Schema.Contents.Count));
                    container.Schema.Contents.Add(component);
                }
            );
        });
    }
}