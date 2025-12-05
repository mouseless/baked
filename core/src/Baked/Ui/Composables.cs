namespace Baked.Ui;

public static class Composables
{
    [Obsolete("Use 'UseRoute' instead.")]
    public static readonly string UseQuery = "useQuery";

    public static ComputedData UseError(
        IData? options = default
    ) => Datas.Computed("useNuxtError", o => o.Options = options);

    public static ComputedData UseRoute(string property) =>
        UseRoute(Datas.Inline(new { property }));

    public static ComputedData UseRoute(
        IData? options = default
    ) => Datas.Computed("useNuxtRoute", o => o.Options = options);
}