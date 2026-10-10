namespace Baked.Ui;

public record NavLink : IComponentSchema
{
    public string Path { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public IData? Query { get; set; }
    public IData? Params { get; set; }
    public string? LabelProp { get; set; }
    public int? MaxLength { get; set; }
    public IComponentDescriptor? Summary { get; set; }
}