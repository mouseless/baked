namespace Baked.Ui;

public static class Composables
{
    [Obsolete("Use 'UseRoute' instead.")]
    public static readonly string UseQuery = "useQuery";

    public static ComputedData UseError() =>
        Datas.Computed("useNuxtError");

    public static ComputedData UseRoute(
        string? property = default
    ) => Datas.Computed("useNuxtRoute", o => o.Options = property is null ? null : Datas.Inline(new { property }));
}