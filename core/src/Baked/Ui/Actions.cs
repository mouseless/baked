namespace Baked.Ui;

public static class Actions
{
    public static CompositeAction Composite(
        Action<CompositeAction>? options = default
    ) => options.Apply(new());

    public static EmitAction Emit(string @event,
        Action<EmitAction>? options = default
    ) => options.Apply(new(@event));

    public static LocalAction Local(string composable,
        Action<LocalAction>? options = default
    ) => options.Apply(new(composable));

    public static RemoteAction Remote(string path, IAction postAction,
        Action<RemoteAction>? options = default
    ) => options.Apply(new(path, postAction));
}