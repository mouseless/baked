namespace Baked.Ui;

public record Icon(string IconClass)
    : IComponentSchema
{
    public string IconClass { get; set; } = IconClass;
}