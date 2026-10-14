using Baked.Architecture;
using Baked.RestApi;
using Baked.Theme.Default;
using Baked.Ui;
using Humanizer;

namespace Baked;

public static class ActionsAsButtonsUxFeature
{
    public static IFeature<UxFeature.Configurator> ActionsAsButtons(this UxFeature.Configurator x,
        int maxParameterCountForConfirm = 3
    ) => x.Define(configurator =>
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            // adds button to methods under actions
            conventions.AddMethodComponent<Button>(
                when: c => c.Method.Has<UiAction>(),
                where: cc => cc.Path.EndsWith("actions", "*")
            );

            // adds redirect action to methods with a route under actions
            conventions.AddMethodSchema(
                when: c => c.Method.Has<UiRoute>(),
                where: cc => cc.Path.EndsWith("actions", "*", "button", "action"),
                schema: c => Execute.Local.UseRedirect(c.Method.Get<UiRoute>().Path)
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

                    ra.PostAction = Execute.Local.UseRedirect(routeBack);
                }
            );

            // adds dialog for parametrized or delete methods that have no route
            conventions.AddMethodSchema<Button.Dialog>(
                when: c =>
                    !c.Method.Has<UiRoute>() &&
                    (
                        c.Method.DefaultOverload.Parameters.Any() ||
                        c.Method.GetAction().Method == HttpMethod.Delete
                    ),
                where: cc => cc.Path.EndsWith("actions", "*", "button", "dialog-options")
            );

            // configures delete or any form with less than max params count for confirm to confirm mode
            conventions.EditMethodSchema<Button.Dialog>(
                when: c =>
                    c.Method.DefaultOverload.Parameters.Count <= maxParameterCountForConfirm ||
                    c.Method.GetAction().Method == HttpMethod.Delete,
                where: cc => cc.Path.EndsWith("actions", "*", "button", "dialog-options"),
                schema: d => d.Mode = Button.DialogMode.Confirm
            );

            // adds confirmation text to methods as dialog content
            conventions.AddMethodComponent<Text>(
                where: cc => cc.Path.EndsWith("button", "dialog-options", "content")
            );
            conventions.EditMethodComponent<Text>(
                where: cc => cc.Path.EndsWith("button", "dialog-options", "content"),
                component: (t, _, cc) =>
                {
                    var (_, l) = cc;

                    t.Data = Fetch.Computed.UseLocalization(l("Are you sure?"));
                }
            );

            // adds simple form to parameterized methods as dialog content
            conventions.AddMethodComponent<SimpleForm>(
                when: c => c.Method.DefaultOverload.Parameters.Any(),
                where: cc => cc.Path.EndsWith("button", "dialog-options", "form")
            );

            // add submit button to any method
            conventions.AddMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("submit")
            );

            // configures submit as primary
            conventions.EditMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("submit"),
                component: b => b.Schema.Severity = "primary"
            );

            // configures post action of remote action to be close dialog under dialog
            conventions.EditMethodSchema<RemoteAction>(
                where: cc => cc.Path.EndsWith("dialog-options", "form", "*", "action"),
                schema: ra => ra.PostAction = Execute.Publish.CloseDialog()
            );

            // configures delete methods as dangerous
            conventions.EditMethodComponent<Button>(
                when: c => c.Method.GetAction().Method == HttpMethod.Delete,
                where: cc => cc.Path.EndsWith("submit"),
                component: b => b.Schema.Severity = "danger"
            );

            // submit is save when in form page
            conventions.EditMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("form-page", "submit"),
                component: (b, _, cc) =>
                {
                    var (_, l) = cc;

                    b.Schema.Label = l("Save");
                }
            );

            // add cancel button for dialog options
            conventions.AddMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("dialog-options", "cancel")
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
            conventions.AddMethodComponent<Button>(
                where: cc => cc.Path.EndsWith("form-page", "title", "page-title", "actions", "back")
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
                schema: () => Execute.Local.UseRedirectBack()
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
                where: cc => cc.Path.EndsWith("actions", "*") && !cc.Path.Contains("form-page"),
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
    });
}