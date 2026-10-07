namespace Baked.Ui;

public record PageSchemaBase(string Path)
    : IPageSchema
{
    public string Path { get; set; } = Path.Trim('/');
    public string? Layout { get; set; }
}