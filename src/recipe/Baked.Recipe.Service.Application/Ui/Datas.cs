using System.Diagnostics.CodeAnalysis;

namespace Baked.Ui;

public static class Datas
{
    public static CompositeData Composite(IEnumerable<IData> parts) =>
        new() { Parts = [.. parts] };

    public static ComputedData Computed(string composable, params IEnumerable<object> args) =>
        new(composable) { Args = [.. args] };

    public static InjectedData Injected() =>
        new();

    [return: NotNullIfNotNull("value")]
    public static InlineData? Inline(object? value) =>
        value != null ? new(value) : null;

    public static RemoteData Remote(string path,
        IData? headers = default,
        IData? query = default
    ) => new(path) { Headers = headers, Query = query };
}