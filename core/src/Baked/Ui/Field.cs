namespace Baked.Ui;

public record Field : IOrderableSchema
{
    public string Key { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public IComponentDescriptor Component { get; set; } = MissingComponent.Empty;
    public bool? Wide { get; set; }
}