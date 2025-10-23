using Baked.Ui;

namespace Baked.Test.Ui;

public record InputText(string TestId)
    : IComponentSchema
{
    public string TestId { get; set; } = TestId;
    public string? DefaultValue { get; set; }
}