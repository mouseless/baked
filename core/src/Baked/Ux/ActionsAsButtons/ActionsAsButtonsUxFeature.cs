using Baked.Architecture;
using Baked.RestApi;
using Baked.Theme;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Theme.Default.DomainComponents;
using static Baked.Ui.Actions;

namespace Baked.Ux.ActionsAsButtons;

public class ActionsAsButtonsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            // `Button`
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<ActionAttribute>() && !c.Method.DefaultOverload.Parameters.Any(),
                where: cc => cc.Path.EndsWith("Actions", "*"),
                component: (c, cc) => MethodButton(c.Method, cc)
            );

            // `SimpleForm` with dialog options
            builder.Conventions.AddMethodComponent(
                when: c =>
                    c.Method.Has<ActionAttribute>() &&
                    (
                        c.Method.DefaultOverload.Parameters.Any() ||
                        c.Method.GetAction().Method == HttpMethod.Delete
                    ),
                where: cc => cc.Path.EndsWith("Actions", "*"),
                component: (c, cc) => MethodSimpleForm(c.Method, cc)
            );
            builder.Conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith("Actions", "*", nameof(SimpleForm), nameof(SimpleForm.DialogOptions)),
                schema: (c, cc) => MethodSimpleFormDialog(c.Method, cc)
            );
            builder.Conventions.AddMethodSchemaConfiguration<SimpleForm.Dialog>(
                when: c => !c.Method.DefaultOverload.Parameters.Any(),
                schema: (sfd, _, cc) =>
                {
                    var (_, l) = cc;

                    sfd.Message = l("Are you sure?");
                }
            );

            // `FormPage` button and routing back
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.TryGet<ActionAttribute>(out var action) && action?.RoutePath is not null,
                where: cc => cc.Path.EndsWith("Actions", "*"),
                component: (c, cc) =>
                {
                    var route = c.Method.Get<ActionAttribute>().RoutePath ?? throw new("`RoutePath` can't be null here");

                    return LocalizedButton(c.Method.Name.Titleize(), cc,
                        action: Local.UseRedirect(route)
                    );
                }
            );
            builder.Conventions.AddMethodSchemaConfiguration<RemoteAction>(
                when: c => c.Method.TryGet<ActionAttribute>(out var action) && action.RoutePathBack is not null,
                where: cc => cc.Path.StartsWith(nameof(Page), "*", "*", nameof(FormPage)),
                schema: (ra, c) =>
                {
                    var routeBack = c.Method.Get<ActionAttribute>().RoutePathBack ?? throw new("`RoutePathBack` can't be null here");

                    ra.PostAction = Local.UseRedirect(routeBack);
                }
            );

            // Inputs of forms
            builder.Conventions.AddMethodComponentConfiguration<SimpleForm>(
                component: (sf, c, cc) =>
                {
                    cc = cc.Drill(nameof(SimpleForm.Inputs));

                    foreach (var parameter in c.Method.DefaultOverload.Parameters)
                    {
                        sf.Schema.Inputs.Add(
                            parameter.GetRequiredSchema<Input>(cc.Drill(parameter.Name))
                        );
                    }
                }
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

            // Open button (for dialog)
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith(nameof(SimpleForm.DialogOptions), nameof(SimpleForm.DialogOptions.Open)),
                component: (c, cc) => LocalizedButton(c.Method.Name.Titleize(), cc)
            );

            // Submit button (for dialog and page)
            builder.Conventions.AddMethodComponent(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc => cc.Path.EndsWith("Submit"),
                component: (c, cc) => LocalizedButton(c.Method.Name.Titleize(), cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith("Submit"),
                component: b => b.Schema.Severity = "primary"
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                when: c => c.Method.GetAction().Method == HttpMethod.Delete,
                where: cc => cc.Path.EndsWith("Submit"),
                component: b => b.Schema.Severity = "danger"
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith(nameof(FormPage), nameof(FormPage.Submit)),
                component: (b, _, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Label = l("Save");
                }
            );

            // Cancel and back buttons
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith(nameof(SimpleForm.DialogOptions), nameof(SimpleForm.DialogOptions.Cancel)),
                component: (_, cc) => LocalizedButton("Cancel", cc)
            );

            builder.Conventions.AddMethodComponentConfiguration<FormPage>(
                component: (fp, c, cc) =>
                {
                    var back = c.Method.GetComponent(cc.Drill(nameof(FormPage), nameof(FormPage.Title), nameof(FormPage.Title.Actions), "Back"));
                    if (back is null) { return; }

                    fp.Schema.Title.Actions.Add(back);
                }
            );
            builder.Conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith(nameof(FormPage), nameof(FormPage.Title), nameof(FormPage.Title.Actions), "Back"),
                component: (_, cc) => LocalizedButton("Back", cc)
            );
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith("Back"),
                component: b => b.Action = Local.UseRedirectBack()
            );

            builder.Conventions.AddMethodComponentConfiguration<Button>(
                where: cc => cc.Path.EndsWith("Cancel") || cc.Path.EndsWith("Back"),
                component: b => b.Schema.Variant = "text"
            );

            // Icons
            builder.Conventions.AddMethodComponentConfiguration<Button>(
                when: c => c.Method.Has<ActionAttribute>(),
                where: cc =>
                    !cc.Path.Contains(nameof(FormPage)) &&
                    (
                        cc.Path.EndsWith("Actions", "*") ||
                        cc.Path.EndsWith("*Dialog*", "Open")
                    ),
                component: (b, c) =>
                {
                    var action = c.Method.GetAction();

                    b.Schema.Icon = action.Method switch
                    {
                        var m when m == HttpMethod.Delete => "pi pi-trash",
                        var m when m == HttpMethod.Patch => "pi pi-pencil",
                        var m when m == HttpMethod.Put => "pi pi-pencil",
                        var m when m == HttpMethod.Post && Regexes.StartsWithAddCreateOrNew.IsMatch(c.Method.Name) => "pi pi-plus",
                        _ => b.Schema.Icon
                    };
                }
            );
        });
    }
}