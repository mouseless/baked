using Baked.Ui;

namespace Baked.Test.Ui;

public record VibeForm : IComponentSchema
{
    public string? Label { get; set; }
    public Action Action { get; set; } = new();
    public string? SubmitEventName { get; set; }
    public List<Parameter> Parameters { get; init; } = [];
}