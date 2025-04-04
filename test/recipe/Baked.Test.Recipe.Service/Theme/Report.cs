namespace Baked.Test.Theme;

public class Report
{
    string _requiredWithDefault = default!;
    string _required = default!;
    string? _optional = default!;

    string Value => $"{_requiredWithDefault} - {_required} - {_optional}";

    public Report With(string requiredWithDefault, string required,
        string? optional = default
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

    public List<ReportRow> GetFirst(CountOptions count = CountOptions.Default) =>
        [.. Enumerable
          .Range(0, (int)count)
          .Select(row => new ReportRow($"Row {row}", _requiredWithDefault, _required, _optional))
        ];

    public List<ReportRow> GetSecond(CountOptions count = CountOptions.Default) =>
        [.. Enumerable
          .Range(0, (int)count)
          .Select(row => new ReportRow($"Row {row}", _requiredWithDefault, _required, _optional))
        ];
}