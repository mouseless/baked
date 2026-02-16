using Newtonsoft.Json;

namespace Baked.Ui;

public record CompositeData : IData
{
    public string Type => "Composite";
    public List<IData> Parts { get; init; } = [];
    public bool? IsAsync => Parts.Any(p => p.IsAsync) ? true : null;
    bool IData.IsAsync => IsAsync == true;

    [JsonIgnore]
    public bool? RequireLocalization { get; set; }
}