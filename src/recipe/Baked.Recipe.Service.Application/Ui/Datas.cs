namespace Baked.Ui;

public static class Datas
{
    public static CompositeData Composite(IEnumerable<IData> parts,
        bool? requireLocalization = default
    ) => new() { Parts = [.. parts], RequireLocalization = requireLocalization };

    public static ComputedData Computed(string composable,
        IEnumerable<object>? args = default,
        bool? requireLocalization = default
    ) => new(composable) { Args = [.. args ?? []], RequireLocalization = requireLocalization };

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
        IEnumerable<(string key, string value)>? options = default,
        bool? requireLocalization = default
    ) => new(path)
    {
        Headers = headers,
        Query = query,
        Options = (options ?? []).ToDictionary(kvp => kvp.key, kvp => kvp.value),
        RequireLocalization = requireLocalization
    };
}