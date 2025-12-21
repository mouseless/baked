using Baked.Architecture;
using Baked.Business;
using Baked.RestApi.Model;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Theme;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainActions;
using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Datas;

using B = Baked.Ui.Components;
using C = Baked.Test.Ui.Components;

namespace Baked.Test.Override.Ui;

// TODO - extract conventions and simplify this
public class FormSampleUiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // contents
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<FormSample>(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage.Contents));

                    foreach (var method in c.Type.GetMembers().Methods.Having<ActionModelAttribute>())
                    {
                        if (method.Has<InitializerAttribute>()) { continue; }
                        if (method.GetAction().Method != HttpMethod.Get) { continue; }

                        var content = method.GetSchema<Content>(cc.Drill(sp.Schema.Contents.Count));
                        if (content is null) { continue; }

                        sp.Schema.Contents.Add(content);
                    }
                }
            );
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
            builder.Conventions.AddMethodComponentConfiguration<FormPage>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill(nameof(FormPage.Inputs));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        sf.Schema.Inputs.Add(
                            parameter.GetRequiredSchema<Input>(cc.Drill(parameter.Name))
                        );
                    }
                }
            );

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

            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.NewParent),
                where: cc => cc.Path.StartsWith(nameof(Page), "*", "*", nameof(FormPage)),
                schema: ra => ra.PostAction = Actions.Local.UseRedirect("/form-sample")
            );

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
                component: (c, cc) => MethodSimpleForm(c.Method, cc,
                    options: sf =>
                    {
                        var (_, l) = cc;

                        sf.DialogTemplate = B.SimpleFormDialogTemplate(
                            new(string.Empty)
                            {
                                Icon = "pi pi-pencil",
                                Variant = "text",
                                Rounded = true
                            },
                            new(l("Cancel"))
                        );
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
        });
    }
}