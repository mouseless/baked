using Newtonsoft.Json;

namespace Baked.Ui;

public record ComputedData()
    : IData
{
    readonly bool _isAsync;

    public ComputedData(bool isAsync)
        : this()
    {
        _isAsync = isAsync;
    }

    public string Type => "Computed";
    public string Composable { get; set; } = string.Empty;
    public IData? Options { get; set; }
    public bool? IsAsync => Options?.IsAsync == true || _isAsync ? true : null;
    bool IData.IsAsync => IsAsync == true;

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }
}