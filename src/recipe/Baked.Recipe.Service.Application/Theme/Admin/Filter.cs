using Baked.Ui;

namespace Baked.Theme.Admin;

public record Filter
    : IComponentSchema
{
    public string? Placeholder { get; set; }
}