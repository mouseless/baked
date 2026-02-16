using Newtonsoft.Json;

namespace Baked.Ui;

public record ComputedData(string Composable)
    : IData
{
    readonly bool _isAsync;

    public ComputedData(string composable, bool isAsync)
        : this(composable)
    {
        _isAsync = isAsync;
    }

    public string Type => "Computed";
    public string Composable { get; set; } = Composable;
    public IData? Options { get; set; }
    public bool? IsAsync => Options?.IsAsync == true || _isAsync ? true : null;
    bool IData.IsAsync => IsAsync == true;

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }
}