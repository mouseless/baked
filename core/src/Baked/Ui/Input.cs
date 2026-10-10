using B = Baked.Ui.Components;

namespace Baked.Ui;

public record Input : IOrderableSchema
{
    public string Name { get; set; } = string.Empty;
    public bool? Required { get; set; }
    public bool? DefaultSelfManaged { get; set; }
    public IData? Default { get; set; }
    public bool? Numeric { get; set; }
    public bool? QueryBound { get; set; }
    public IComponentDescriptor Component { get; set; } = B.MissingComponent();

    public object? DefaultValue { set => Default = value is not null && value != DBNull.Value ? Datas.Inline(value) : null; }
    string IOrderableSchema.Key => Name;
}