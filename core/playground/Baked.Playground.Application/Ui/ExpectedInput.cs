using Baked.Ui;

namespace Baked.Playground.Ui;

public record ExpectedInput(string TestId)
    : IComponentSchema
{
    public string TestId { get; set; } = TestId;
    public string? DefaultValue { get; set; }
    public bool? Number { get; set; }
}