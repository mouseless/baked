namespace Baked.Ui.Component;

public record Icon(string IconClass)
    : IComponentSchema
{
    public string IconClass { get; set; } = IconClass;
}