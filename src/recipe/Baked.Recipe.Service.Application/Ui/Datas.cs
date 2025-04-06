namespace Baked.Ui;

public static class Datas
{
    public static CompositeData Composite(IEnumerable<IData> parts) =>
        new() { Parts = [.. parts] };

    public static ComputedData Computed(string composable, params IEnumerable<object> args) =>
        new(composable) { Args = [.. args] };

    public static InjectedData Injected(
        InjectedData.DataKey key = InjectedData.DataKey.Custom,
        string? prop = default
    ) => new(key) { Prop = prop };

    public static InlineData Inline(object value) =>
        new(value);

    public static RemoteData Remote(string path,
        IData? headers = default,
        IData? query = default
    ) => new(path) { Headers = headers, Query = query };
}