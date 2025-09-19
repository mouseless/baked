using Baked.Ui;

namespace Baked.Theme.Default;

public record Icon(string IconClass)
    : IComponentSchema
{
    public string IconClass { get; set; } = IconClass;
}