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
        public ContextData Parent(
            Action<ContextData>? options = default
        ) => options.Apply(new("parent") { Prop = "data" });

        public ContextData Model(
            Action<ContextData>? options = default
        ) => options.Apply(new("model"));
    }

    public class Composables
    {
        public ComputedData UseError(
           IData? options = default
        ) => Use("NuxtError", o => o.Options = options);

        public ComputedData UseRoute(string property) =>
            UseRoute(Inline(new { property }));

        public ComputedData UseRoute(
            IData? options = default
        ) => Use("NuxtRoute", o => o.Options = options);

        public ComputedData Use(string composable,
            Action<ComputedData>? options = default
        )
        {
            composable = composable.StartsWith("use") ? composable : $"use{composable}";
            var result = new ComputedData(composable);

            options?.Invoke(result);

            return result;
        }
    }
}