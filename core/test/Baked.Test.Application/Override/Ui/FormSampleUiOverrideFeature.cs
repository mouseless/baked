using Baked.Architecture;
using Baked.Business;
using Baked.RestApi.Model;
using Baked.Test.Orm;
using Baked.Test.Theme;
using Baked.Test.Ui;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

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
            // TODO review in conventions
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<Parent>(),
                component: (sp) => sp.Data = Remote("/parents/{id}", o => o.Params = Computed.UseRoute("params"))
            );
            builder.Conventions.AddTypeComponentConfiguration<SimplePage>(
                when: c => c.Type.Is<Parent>(),
                component: (sp, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimplePage.Contents));

                    foreach (var property in c.Type.GetMembers().Properties)
                    {
                        var content = property.GetSchema<Content>(cc.Drill(sp.Schema.Contents.Count));
                        if (content is null) { continue; }

                        sp.Schema.Contents.Add(content);
                    }
                }
            );
            builder.Conventions.AddPropertySchema(
                when: c => c.Type.Is<Parent>(),
                where: cc => cc.Path.EndsWith(nameof(Page), "*", nameof(SimplePage.Contents), "*"),
                schema: (c, cc) => PropertyContent(c.Property, cc)
            );
            builder.Conventions.AddPropertyComponentConfiguration<Text>(
                when: c => c.Type.Is<Parent>(),
                where: cc => cc.Path.EndsWith(nameof(Page), "*", nameof(SimplePage.Contents), "*", "*", nameof(Content.Component)),
                component: (t, c) => t.Data = Context.Parent(o => o.Prop = $"data.{c.Property.Get<DataAttribute>().Prop}")
            );
            builder.Conventions.AddTypeComponent(
                when: c => c.Type.Has<LocatableAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate), nameof(Container), nameof(Container.Contents)),
                component: (c) => B.NavLink($"/{c.Type.Name.Pluralize().Kebaberize()}/{{id}}", "id", "name")
            );
            builder.Conventions.AddTypeComponentConfiguration<NavLink>(
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate), nameof(Container), nameof(Container.Contents)),
                component: link =>
                {
                    link.Schema.TextProp = string.Empty;
                    link.Schema.Icon = "pi pi-eye";
                    link.Data = Context.Parent(options: o => o.Prop = "row");
                }
            );
            // END TODO

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
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Delete),
                where: cc => cc.Path.EndsWith(nameof(DataTable.ActionTemplate), "**"),
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
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Update),
                where: cc => cc.Path.EndsWith(nameof(DataTable.ActionTemplate), "**"),
                component: (c, cc) => MethodSimpleForm(c.Method, cc)
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<Parent>() && c.Method.Name == nameof(Parent.Update),
                where: cc => cc.Path.EndsWith(nameof(DataTable.ActionTemplate), "**"),
                schema: (c, cc) => MethodSimpleFormDialog(c.Method, cc)
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
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate)),
                schema: (c, cc) => B.DataTableColumn("Actions")
            );
            builder.Conventions.AddMethodSchemaConfiguration<DataTable.Column>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name == nameof(FormSample.GetParents),
                where: cc => cc.Path.EndsWith(nameof(DataTable), nameof(DataTable.ActionTemplate)),
                schema: (col, c, cc) =>
                {
                    if (!c.Method.DefaultOverload.ReturnType.TryGetGenerics(out var generics)) { return; }

                    cc = cc.Drill(nameof(Container), nameof(Container.Contents));

                    var returnType = generics.GenericTypeArguments.First().Model;
                    var rowActions = new List<IComponentDescriptor>();
                    foreach (var method in returnType.GetMembers().Methods)
                    {
                        var component = method.GetComponent(cc.Drill(method.Name));
                        if (component is null) { continue; }

                        rowActions.Add(component);
                    }

                    // TODO review in conventions
                    var link = returnType.GetMetadata().GetComponent<NavLink>(cc);
                    if (link is not null)
                    {
                        rowActions.Insert(0, link);
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