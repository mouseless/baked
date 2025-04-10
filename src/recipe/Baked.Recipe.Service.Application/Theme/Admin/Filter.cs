using Baked.Ui;

namespace Baked.Theme.Admin;

public record Filter(string PageContextKey)
    : IComponentSchema
{
    public string PageContextKey { get; set; } = PageContextKey;
    public string? Placeholder { get; set; }
}