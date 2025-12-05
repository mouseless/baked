namespace Baked.Ui;

public static class Datas
{
    public static CompositeData Composite(
        Action<CompositeData>? options = default
    ) => options.Apply(new());

    public static ComputedData Computed(string composable,
        Action<ComputedData>? options = default
    ) => options.Apply(new(composable));

    public static ContextData Context(
        Action<ContextData>? options = default
    ) => options.Apply(new());

    public static InlineData Inline(object value,
        Action<InlineData>? options = default
    ) => options.Apply(new(value));

    public static RemoteData Remote(string path,
        Action<RemoteData>? options = default
    ) => options.Apply(new(path));
}