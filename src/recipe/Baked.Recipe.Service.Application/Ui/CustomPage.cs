namespace Baked.Ui;

public record CustomPage(string Name, string? Layout, string? Route)
    : INamedComponentSchema
{
    public string Name { get; } = Name;
    public string? Layout { get; } = Layout;
    public string Route { get; } = Route ?? string.Empty;
}