namespace Baked.Ui;

public static class Fetch
{
    public static Composables Computed { get; } = new();
    public static ContextDatas Context { get; } = new();

    public static CompositeData Composite(
        Action<CompositeData>? options = default
    ) => options.Apply(new());

    public static InlineData Inline(object value,
        Action<InlineData>? options = default
    ) => options.Apply(new() { Value = value });

    public static RemoteData Remote(string path,
        Action<RemoteData>? options = default
    ) => options.Apply(new() { Path = path });

    public class ContextDatas
    {
        public ContextData Model(
            Action<ContextData>? options = default
        ) => options.Apply(new() { Key = "model" });

        public ContextData Page(
            Action<ContextData>? options = default
        ) => options.Apply(new() { Key = "page" });

        public ContextData Parent(
            Action<ContextData>? options = default
        ) => options.Apply(new() { Key = "parent", Prop = "data" });

        public ContextData Response(
            Action<ContextData>? options = default
        ) => options.Apply(new() { Key = "response" });
    }

    public class Composables
    {
        public ComputedData UseError(
            Action<ComputedData>? options = default
        ) => Use("NuxtError", options: options);

        public ComputedData UseLoginRedirect(
            Action<ComputedData>? options = default
        ) => Use("LoginRedirect", options: options);

        public ComputedData UseRoute(string property) =>
            UseRoute(options: cd => cd.Options = Inline(new { property }));

        public ComputedData UseRoute(
            Action<ComputedData>? options = default
        ) => Use("NuxtRoute", options: options);

        public ComputedData Use(string composable,
            bool isAsync = false,
            Action<ComputedData>? options = default
        ) => options.Apply(new(isAsync) { Composable = composable.StartsWith("use") ? composable : $"use{composable}" });
    }
}