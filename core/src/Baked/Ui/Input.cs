namespace Baked.Ui;

public record Input(string Name, IComponentDescriptor Component)
{
    public string Name { get; set; } = Name;
    public bool? Required { get; set; }
    public bool? DefaultSelfManaged { get; set; }
    public IData? Default { get; set; }
    public IComponentDescriptor Component { get; set; } = Component;

    public object? DefaultValue { set => Default = value is not null ? Datas.Inline(value) : null; }
}