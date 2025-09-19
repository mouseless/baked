namespace Baked.Ui;

public record PageSchemaBase(string Path)
    : IPageSchema
{
    public string Path { get; set; } = Path;
    public string? Layout { get; set; }
}