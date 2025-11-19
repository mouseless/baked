namespace Baked.Ui;

public static class Actions
{
    public static CompositeAction Composite(
        Action<CompositeAction>? options = default
    ) => options.Apply(new());

    public static LocalAction Local(string composable,
        Action<LocalAction>? options = default
    ) => options.Apply(new(composable));

    public static RemoteAction Remote(string path,
        Action<RemoteAction>? options = default
    ) => options.Apply(new(path));
}