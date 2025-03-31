namespace Baked.Ui;

public record CustomPage(string Name, string? Layout)
    : INamedComponentSchema
{
    public string Name { get; } = Name;
    public string? Layout { get; } = Layout;
}