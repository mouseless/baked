using Baked.Ui;

namespace Baked.Theme.Admin;

public record Icon(string IconClass)
    : IComponentSchema
{
    public string IconClass { get; set; } = IconClass;
}