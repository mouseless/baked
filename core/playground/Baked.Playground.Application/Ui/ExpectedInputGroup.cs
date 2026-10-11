using Baked.Ui;

namespace Baked.Playground.Ui;

public record ExpectedInputGroup
    : IComponentSchema
{
    public string TestId { get; set; } = string.Empty;
}