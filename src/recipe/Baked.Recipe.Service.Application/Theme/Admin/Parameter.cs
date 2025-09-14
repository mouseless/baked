using Baked.Ui;

namespace Baked.Theme.Admin;

public record Parameter(string Name, IComponentDescriptor Component)
{
    public string Name { get; set; } = Name;
    public bool? Required { get; set; }
    public bool? DefaultSelfManaged { get; set; }
    public IData? Default { get; set; }
    public IComponentDescriptor Component { get; set; } = Component;

    public object? DefaultValue { set => Default = value is not null ? Datas.Inline(value) : null; }
}