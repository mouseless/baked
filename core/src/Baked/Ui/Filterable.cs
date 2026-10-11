namespace Baked.Ui;

public record Filterable
{
    public string Title { get; set; } = string.Empty;
    public IComponentDescriptor Component { get; set; } = MissingComponent.Empty;
}