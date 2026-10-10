using Baked.Architecture;
using Baked.RestApi;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

using static Baked.Ui.Actions;

using B = Baked.Ui.Components;

namespace Baked.Ux.ActionsAsButtons;

public class ActionsAsButtonsUxFeature : IFeature<UxConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // `Button`
            conventions.AddMethodComponent(
                when: c => c.Method.Has<UiAction>() && !c.Method.DefaultOverload.Parameters.Any(),
                where: cc => cc.Path.EndsWith("actions", "*"),
                component: () => B.Button()
            );

            // `SimpleForm` with dialog options
            conventions.AddMethodComponent(
                when: c =>
                    c.Method.Has<UiAction>() &&
                    (
                        c.Method.DefaultOverload.Parameters.Any() ||
                        c.Method.GetAction().Method == HttpMethod.Delete
                    ),
                where: cc => cc.Path.EndsWith("actions", "*"),
                component: () => B.SimpleForm()
            );
            conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith("actions", "*", "simple-form", "dialog-options"),
                schema: () => B.SimpleFormDialog()
            );
            conventions.EditMethodSchema<SimpleForm.Dialog>(
                when: c => !c.Method.DefaultOverload.Parameters.Any(),
                schema: (sfd, _, cc) =>
                {
                    var (_, l) = cc;

                    sfd.Message = l("Are you sure?");
                }
            );

            // adds button to the methods with a route
            conventions.AddMethodComponent(
                when: c => c.Method.Has<UiAction>() && c.Method.Has<UiRoute>(),
                where: cc => cc.Path.EndsWith("actions", "*"),
                component: () => B.Button()
            );

            // adds redirect action for methods with a route
            conventions.AddMethodSchema(
                when: c => c.Method.Has<UiAction>() && c.Method.Has<UiRoute>(),
                where: cc => cc.Path.EndsWith("actions", "*", "button", "action"),
                schema: c => Local.UseRedirect(c.Method.Get<UiRoute>().Path)
            );

            // configures post action to be a redirect back to the configured route path back for methods under the form page
            conventions.EditMethodSchema<RemoteAction>(
                when: c => c.Method.TryGet<UiAction>(out var action) && action.RoutePathBack is not null,
                where: cc => cc.Path.StartsWith("page", "*", "*", "form-page"),
                schema: (ra, c) =>
                {
                    var routeBack =
                        c.Method.Get<UiAction>().RoutePathBack ??
                        throw DiagnosticCode.InvalidState.Exception(
                            $"`{nameof(UiAction.RoutePathBack)}` can't be null here"
                        );

                    ra.PostAction = Local.UseRedirect(routeBack);
                }
            );

            // Open button (for dialog)
            conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith("dialog-options", "open"),
                component: () => B.Button()
            );

            // Submit button (for dialog and page)
            conventions.AddMethodComponent(
                when: c => c.Method.Has<UiAction>(),
                where: cc => cc.Path.EndsWith("submit"),
                component: () => B.Button()
            );
            conventions.EditMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("submit"),
                component: b => b.Schema.Severity = "primary"
            );
            conventions.EditMethodComponent<Button>(
                when: c => c.Method.GetAction().Method == HttpMethod.Delete,
                where: cc => cc.Path.EndsWith("submit"),
                component: b => b.Schema.Severity = "danger"
            );
            conventions.EditMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("form-page", "submit"),
                component: (b, _, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Label = l("Save");
                }
            );

            // add cancel button for dialog options
            conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith("dialog-options", "cancel"),
                component: () => B.Button()
            );

            // configures back button on form-page
            conventions.EditMethodComponent<PageTitle>(
                where: cc => cc.Path.StartsWith("page", "*", "*", "form-page"),
                component: (fp, c, cc) =>
                {
                    var back = c.Method.GenerateComponent(cc.Drill("page-title", "actions", "back"));
                    if (back is null) { return; }

                    fp.Schema.Actions.Add(back);
                }
            );

            // adds back button to form page
            conventions.AddMethodComponent(
                where: cc => cc.Path.EndsWith("form-page", "title", "page-title", "actions", "back"),
                component: () => B.Button()
            );

            // configure label for cancel & button
            conventions.EditMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("cancel") || cc.Path.EndsWith("back"),
                component: (b, _, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Label = l(cc.Path.GetParts().Last().Titleize());
                }
            );

            // adds redirect back action to back button
            conventions.AddMethodSchema(
                where: cc => cc.Path.EndsWith("back", "button", "action"),
                schema: () => Local.UseRedirectBack()
            );

            // clears action of cancel button
            conventions.EditMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("cancel"),
                component: b => b.Action = null
            );

            // configures text variant for cancel and back
            conventions.EditMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("cancel") || cc.Path.EndsWith("back"),
                component: b => b.Schema.Variant = "text"
            );

            // Icons
            conventions.EditMethodComponent<Button>(
                when: c => c.Method.Has<UiAction>(),
                where: cc =>
                    !cc.Path.Contains("form-page") &&
                    (
                        cc.Path.EndsWith("actions", "*") ||
                        cc.Path.EndsWith("*dialog*", "open")
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