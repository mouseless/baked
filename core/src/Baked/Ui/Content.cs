namespace Baked.Ui;

public record Content(IComponentDescriptor Component, string Key)
{
    public IComponentDescriptor Component { get; set; } = Component;
    public string Key { get; set; } = Key;
    public bool? Narrow { get; set; }
}