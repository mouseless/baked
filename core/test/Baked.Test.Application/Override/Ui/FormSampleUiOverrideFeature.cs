using Baked.Architecture;
using Baked.RestApi.Model;
using Baked.Test.Theme;
using Baked.Test.Ui;
using Baked.Ui;
using Microsoft.AspNetCore.Mvc.RazorPages;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Test.Theme.Custom.DomainComponents;

using C = Baked.Test.Ui.Components;
using DA = Baked.Theme.Default.DomainActions;

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
                    c.Method.Name.StartsWith("Get") &&
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Having<ActionModelAttribute>().Any(m => !m.Name.StartsWith("Get")),
                component: dp =>
                {
                    dp.Schema.Content.Binding = "something-changed";
                }
            );
            builder.Conventions.AddMethodComponent(
                when: c => !c.Method.Name.StartsWith("Get"),
                where: cc => cc.Path.EndsWith(nameof(DataPanel), nameof(DataPanel.Content)),
                component: c => C.VibeForm(options: vf =>
                {
                    var action = c.Method.GetAction();

                    vf.Label = c.Method.Name;
                    vf.Action.Path = action.GetRoute();
                    vf.Action.Method = action.Method.ToString().ToUpperInvariant();
                    vf.SubmitEventName = "something-changed";
                })
            );
            builder.Conventions.AddMethodComponentConfiguration<VibeForm>(
                component: (vf, c, cc) =>
                {
                    cc = cc.Drill(nameof(VibeForm));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        vf.Schema.Inputs.Add(ParameterInput(parameter, cc.Drill(nameof(VibeForm.Inputs)), options: i =>
                        {
                            i.Required = !parameter.IsOptional;
                        }));
                    }
                }
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.Is<string>(),
                component: c => C.InputText(c.Parameter.Name)
            );
            builder.Conventions.AddParameterComponent(
                when: c => c.Parameter.ParameterType.Is<int>(),
                component: c => C.InputNumber(c.Parameter.Name)
            );
            // END OF TODO - review this in form components

            builder.Conventions.RemoveMethodSchema<ReportPage.Tab.Content>(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.ClearStates))
            );
            builder.Conventions.AddMethodSchema(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.ClearStates)),
                where: cc => true,
                schema: (c, _) => DA.MethodRemote(c.Method)
            );
            builder.Conventions.AddMethodComponent(
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.ClearStates)),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions)),
                component: (c, cc) => MethodButton(c.Method, cc.Drill(c.Method.Name))
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                when: c => c.Method.Name.Equals(nameof(FormSample.ClearStates)),
                where: cc => cc.Path.EndsWith(nameof(PageTitle.Actions)),
                component: button =>
                {
                    button.PostAction = Actions.Emit("something-changed");
                }
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
                when: c => c.Type.Is<FormSample>() && c.Method.Name.Equals(nameof(FormSample.AddState)),
                where: cc => cc.Path.EndsWith(nameof(Page), nameof(FormSample), nameof(FormSample.AddState)),
                component: (c, cc) => MethodContainerPage(c.Method, cc.Drill(c.Method.Name))
            );
            builder.Conventions.AddMethodComponent(
                when: c => !c.Method.Name.StartsWith("Get") && c.Method.Has<ActionModelAttribute>(),
                where: cc => cc.Path.EndsWith(nameof(FormSample), nameof(FormSample.AddState), nameof(ContainerPage), nameof(ContainerPage.Contents), "*"),
                component: c => C.VibeForm(options: vf =>
                {
                    var action = c.Method.GetAction();

                    vf.Label = c.Method.Name;
                    vf.Action.Path = action.GetRoute();
                    vf.Action.Method = action.Method.ToString().ToUpperInvariant();
                    vf.SubmitEventName = "something-changed";
                })
            );
            // TODO - move to default feature
            builder.Conventions.AddMethodComponentConfiguration<ContainerPage>(
                where: cc => cc.Path.Contains(nameof(FormSample), nameof(FormSample.AddState)),
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