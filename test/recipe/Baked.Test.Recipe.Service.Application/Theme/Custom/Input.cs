using Baked.Ui;

namespace Baked.Test.Theme.Custom;

public record Input(string TestId)
    : IComponentSchema
{
    public string TestId { get; set; } = TestId;
    public string? DefaultValue { get; set; }
}