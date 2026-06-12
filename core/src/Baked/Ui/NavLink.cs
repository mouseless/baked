namespace Baked.Ui;

public record NavLink(string Path)
    : IComponentSchema
{
    public string Path { get; set; } = Path;
    public string? Icon { get; set; }
    public IData? Query { get; set; }
    public IData? Params { get; set; }
    public string? LabelProp { get; set; }
}