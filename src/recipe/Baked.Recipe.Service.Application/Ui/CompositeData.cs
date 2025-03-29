namespace Baked.Ui;

public record CompositeData()
    : IData
{
    public string Type => "Composite";
    public List<IData> Parts { get; init; } = [];
}