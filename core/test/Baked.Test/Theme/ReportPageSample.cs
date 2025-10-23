namespace Baked.Test.Theme;

public class ReportPageSample
{
    RequiredWithDefaultOptions _requiredWithDefault = default!;
    RequiredOptions _required = default!;
    OptionalOptions? _optional = default!;

    string Value => $"{_requiredWithDefault} - {_required} - {_optional}";

    public ReportPageSample With(RequiredWithDefaultOptions requiredWithDefault, RequiredOptions required,
        OptionalOptions? optional = default
    )
    {
        _requiredWithDefault = requiredWithDefault;
        _required = required;
        _optional = optional;

        return this;
    }

    public string GetWide() =>
        $"WIDE: {Value}";

    public string GetLeft() =>
        $"LEFT: {Value}";

    public string GetRight() =>
        $"RIGHT: {Value}";

    public async Task<List<ReportRow>> GetFirst(CountOptions count = CountOptions.Default)
    {
        await Task.Delay(200);

        return
        [
            .. Enumerable
                .Range(0, (int)count)
                .Select(row => new ReportRow($"id-{row}", $"Row {row}", $"{_requiredWithDefault}", $"{_required}", $"{_optional}"))
        ];
    }

    public async Task<List<ReportRow>> GetSecond(CountOptions count = CountOptions.Default)
    {
        await Task.Delay(200);

        return
        [
            .. Enumerable
                .Range(0, (int)count)
                .Select(row => new ReportRow($"id-{row}", $"Row {row}", $"{_requiredWithDefault}", $"{_required}", $"{_optional}"))
        ];
    }
}