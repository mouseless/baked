using B = Baked.Ui.Components;

namespace Baked.Ui;

public record Dialog : IComponentSchema
{
    public IComponentDescriptor Content { get; set; } = B.MissingComponent();
    public Button Open { get; set; } = new();
    public string Header { get; set; } = string.Empty;
    public Button? Submit { get; set; }
}