namespace Baked.Ui;

public static class Datas
{
    public static Composables Computed { get; } = new();
    public static ContextDatas Context { get; } = new();

    public static CompositeData Composite(
        Action<CompositeData>? options = default
    ) => options.Apply(new());

    public static InlineData Inline(object value,
        Action<InlineData>? options = default
    ) => options.Apply(new(value));

    public static RemoteData Remote(string path,
        Action<RemoteData>? options = default
    ) => options.Apply(new(path));

    public class ContextDatas
    {
        public ContextData Model(
            Action<ContextData>? options = default
        ) => options.Apply(new("model"));

        public ContextData Page(
            Action<ContextData>? options = default
        ) => options.Apply(new("page"));

        public ContextData Parent(
            Action<ContextData>? options = default
        ) => options.Apply(new("parent") { Prop = "data" });

        public ContextData Response(
            Action<ContextData>? options = default
        ) => options.Apply(new("response"));
    }

    public class Composables
    {
        public ComputedData UseError(
            Action<ComputedData>? options = default
        ) => Use("NuxtError", options: options);

        public ComputedData UseRoute(string property) =>
            UseRoute(options: cd => cd.Options = Inline(new { property }));

        public ComputedData UseRoute(
            Action<ComputedData>? options = default
        ) => Use("NuxtRoute", options);

        public ComputedData Use(string composable,
            Action<ComputedData>? options = default
        ) => options.Apply(new(composable.StartsWith("use") ? composable : $"use{composable}"));
    }
}