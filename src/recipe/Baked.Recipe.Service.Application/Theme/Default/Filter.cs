using Baked.Ui;

namespace Baked.Theme.Default;

public record Filter(string PageContextKey)
    : IComponentSchema
{
    public string PageContextKey { get; set; } = PageContextKey;
    public string? Placeholder { get; set; }
}