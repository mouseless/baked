using Baked.Ui;

namespace Baked.Theme.Admin;

public record Search
    : IComponentSchema
{
    public string? Placeholder { get; set; }
}