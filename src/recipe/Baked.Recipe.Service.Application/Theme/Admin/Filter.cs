using Baked.Ui;

namespace Baked.Theme.Admin;

public record Filter(string ContextKey)
    : IComponentSchema
{
    public string ContextKey { get; set; } = ContextKey;
    public string? Placeholder { get; set; }
}