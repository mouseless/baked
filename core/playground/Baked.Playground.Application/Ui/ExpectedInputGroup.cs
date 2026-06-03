using Baked.Ui;

namespace Baked.Playground.Ui;

public record ExpectedInputGroup(string TestId)
    : IComponentSchema
{
    public string TestId { get; set; } = TestId;
}