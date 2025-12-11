using static Baked.Ui.Datas;

namespace Baked.Ui;

public static class Actions
{
    public static Composables Local { get; } = new();

    public static CompositeAction Composite(
        Action<CompositeAction>? options = default
    ) => options.Apply(new());

    public static EmitAction Emit(string @event,
        Action<EmitAction>? options = default
    ) => options.Apply(new(@event));

    public static RemoteAction Remote(string path, IAction postAction,
        Action<RemoteAction>? options = default
    ) => options.Apply(new(path, postAction));

    public class Composables
    {
        public LocalAction UseRedirect(string route) =>
            UseRedirect(options: la => la.Options = Inline(new { route }));

        public LocalAction UseRedirect(
            Action<LocalAction>? options = default
        ) => Use("Redirect", options);

        public LocalAction Use(string composable,
            Action<LocalAction>? options = default
        ) => options.Apply(new(composable.StartsWith("use") ? composable : $"use{composable}"));
    }
}