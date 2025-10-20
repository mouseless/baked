namespace Baked.Ui;

public record Filter(string PageContextKey)
    : IComponentSchema
{
    public string PageContextKey { get; set; } = PageContextKey;
    public string? Placeholder { get; set; }
}