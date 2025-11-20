namespace Baked.Ui;

public static class Composables
{
    public static readonly string UseEmitEvent = "useEmitEvent";
    public static readonly string UseError = "useNuxtError";
    [Obsolete("Use 'UseRoute' instead.")]
    public static readonly string UseQuery = "useQuery";
    public static readonly string UseRoute = "useNuxtRoute";
}