namespace Baked.Ui;

public static class Datas
{
    public static CompositeData Composite(IEnumerable<IData> parts) =>
        new() { Parts = [.. parts] };

    public static ComputedData Computed(string composable, params IEnumerable<object> args) =>
        new(composable) { Args = [.. args] };

    public static InjectedData Injected(
        bool? custom = default,
        bool? parentData = default,
        string? prop = default,
        bool? requireLocalization = default
    ) => new(
        custom == true ? InjectedData.DataKey.Custom :
        parentData == true ? InjectedData.DataKey.ParentData :
        InjectedData.DataKey.Custom
    )
    { Prop = prop, RequireLocalization = requireLocalization };

    public static InlineData Inline(object value,
        bool requireLocalization = true
    ) => new(value) { RequireLocalization = requireLocalization };

    public static RemoteData Remote(string path,
        IData? headers = default,
        IData? query = default,
        bool? requireLocalization = default
    ) => new(path) { Headers = headers, Query = query, RequireLocalization = requireLocalization };
}