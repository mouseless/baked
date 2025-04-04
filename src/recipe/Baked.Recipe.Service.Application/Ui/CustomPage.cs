namespace Baked.Ui;

public record CustomPage(string Path, string? Layout)
    : IGeneratedComponentSchema
{
    public string Path { get; } = Path;
    public string? Layout { get; } = Layout;
}