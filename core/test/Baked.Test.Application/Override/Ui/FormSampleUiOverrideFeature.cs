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
            // below this point is vibe coding
            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                when: c => c.Type.Is<FormSample>(),
                component: (rp, c, cc) =>
                {
                    var forms = new List<ReportPage.Tab.Content>();
                    var firstTab = rp.Schema.Tabs.First();

                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionModelAttribute>())
                    {
                        var action = method.GetAction();
                        if (action.Method == HttpMethod.Get) { continue; }

                        var schema = method.GetSchema<ReportPage.Tab.Content>(
                            cc.Drill(nameof(ReportPage.Tabs), firstTab.Id, nameof(ReportPage.Tab.Contents), firstTab.Contents.Count + forms.Count)
                        );

                        if (schema is null) { continue; }

                        forms.Add(schema);
                    }

                    firstTab.Contents.InsertRange(0, forms);
                }
            );
            builder.Conventions.AddMethodSchemaConfiguration<ReportPage.Tab.Content>(
                when: c => !c.Method.Name.StartsWith("Get"),
                schema: rptc => rptc.Narrow = true
            );
            builder.Conventions.AddMethodComponentConfiguration<DataPanel>(
                when: c =>
                    c.Type.Is<FormSample>() &&
                    c.Method.Name.StartsWith("Get") &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ActionModelAttribute>().Any(m => !m.Name.StartsWith("Get")),
                component: dp =>
                {
                    dp.Schema.Content.ReloadOn(nameof(FormSample.ClearParents).Kebaberize());
                    dp.Schema.Content.ReloadOn(nameof(Parent.Delete).Kebaberize());
                    dp.Schema.Content.ReloadOn(nameof(Parent.Update).Kebaberize());
                }
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => true,
                schema: (c, _) => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => true,
                schema: ra =>
                {
                    ra.Body = Context.Model();
                }
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Type.Is<FormSample>() && c.Parameter.ParameterType.Is<string>(),
                component: c => C.InputText(c.Parameter.Name)
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Type.Is<Parent>() && c.Parameter.ParameterType.Is<string>(),
                component: c => C.InputText(c.Parameter.Name)
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Type.Is<FormSample>() && c.Parameter.ParameterType.Is<int>(),
                component: c => C.InputNumber(c.Parameter.Name)
            );
            builder.Conventions.AddMethodComponentConfiguration<SimpleForm>(
               component: (sf, c, cc) =>
               {
                   Console.WriteLine(c.Method.Name);
                   sf.Action = c.Method.GetRequiredSchema<RemoteAction>(cc.Drill(nameof(IComponentDescriptor.Action)));

                   cc = cc.Drill(nameof(SimpleForm));

                   foreach (var parameter in c.Method.DefaultOverload.Parameters)
                   {
                       sf.Schema.Inputs.Add(ParameterInput(parameter, cc.Drill(nameof(SimpleForm.Inputs)), options: i =>
                       {
                           i.Required = !parameter.IsOptional;
                       }));
                   }
               }
           );
            // END OF TODO - review this in form components

            builder.Conventions.RemoveMethodSchema<ReportPage.Tab.Content>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.ClearParents)) || c.Method.Name.Equals(nameof(FormSample.NewParent))
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.ClearParents)),
                where: cc => true,
                schema: (c, _) => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.ClearParents)),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions)),
                component: (c, cc) => MethodButton(c.Method, cc.Drill(c.Method.Name))
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions)),
                component: (c, cc) => MethodButton(c.Method, cc.Drill(c.Method.Name))
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions)),
                component: b => b.Action = Actions.Local.UseRedirect("/form-sample/new-parent")
            );

            builder.Conventions.AddTypeComponentConfiguration<ReportPage>(
                when: c => c.Type.Is<FormSample>(),
                component: (rp, c, cc) =>
                {
                    cc = cc.Drill(nameof(ReportPage), nameof(ReportPage.Title));
                    foreach (var method in c.Type.GetMembers().Methods)
                    {
                        var component = method.GetComponent(cc.Drill(nameof(PageTitle.Actions)));
                        if (component is null) { continue; }

                        rp.Schema.Title.Actions.Add(component);
                    }
                }
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.NewParent)),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(FormSample), nameof(FormSample.NewParent)),
                component: (c, cc) => MethodContainerPage(c.Method, cc.Drill(c.Method.Name))
            );
            builder.Conventions.AddMethodComponent(
                when: c => !c.Method.Name.StartsWith("Get") && c.Method.Has<ActionModelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(FormSample), nameof(FormSample.NewParent), nameof(ContainerPage), nameof(ContainerPage.Contents), "*"),
                component: c => B.SimpleForm(options: sf => sf.ButtonLabel = c.Method.Name)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                schema: ra =>
                {
                    ra.PostAction = Actions.Local.UseRedirect("/form-sample");
                }
            );

            // TODO row action
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<Parent>() && (c.Method.Name == nameof(Parent.Delete) || c.Method.Name == nameof(Parent.Update)),
                schema: (c, cc) => MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<Parent>() && (c.Method.Name == nameof(Parent.Delete) || c.Method.Name == nameof(Parent.Update)),
                schema: ra => ra.Params = Context.Parent(options: o => o.Prop = "row")
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Update),
                schema: ra => ra.Body = Context.Model()
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

            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Update),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate)),
                component: c => B.SimpleForm(options: sf =>
                {
                    sf.ButtonLabel = c.Method.Name;
                    sf.Dialog = true;
                    sf.ButtonIcon = "pi pi-pencil";
                    sf.ButtonVariant = "text";
                    sf.ButtonRounded = true;
                })
            );

            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate)),
                schema: (c, cc) => B.DataTableColumn("Actions")
            );
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate)),
                schema: (col, c, cc) =>
                {
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
                    col.Component = C.Container(options: c => c.Contents.AddRange(rowActions));
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