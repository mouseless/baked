using Baked.Ui;

namespace Baked.Playground.Ui;

public record ExpectedInput : IComponentSchema
{
    public string TestId { get; set; } = string.Empty;
    public string? DefaultValue { get; set; }
    public bool? Number { get; set; }
}