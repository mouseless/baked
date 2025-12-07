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
            UseRedirect(Datas.Inline(new { route }));

        public LocalAction UseRedirect(IData options) =>
            Use("Redirect", o => o.Options = options);

        public LocalAction Use(string composable,
            Action<LocalAction>? options = default
        )
        {
            composable = composable.StartsWith("use") ? composable : $"use{composable}";
            var result = new LocalAction(composable);

            options?.Invoke(result);

            return result;
        }
    }
}