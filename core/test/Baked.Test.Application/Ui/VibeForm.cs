using Baked.Ui;

namespace Baked.Test.Ui;

// TODO - review this in form components
public record VibeForm : IComponentSchema
{
    public string? Label { get; set; }
    public Action Action { get; set; } = new();
    public string? SubmitEventName { get; set; }
    public List<Input> Inputs { get; init; } = [];
}