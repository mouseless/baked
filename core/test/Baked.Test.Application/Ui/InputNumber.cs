using Baked.Ui;

namespace Baked.Test.Ui;

public record InputNumber(string TestId)
    : IComponentSchema
{
    public string TestId { get; set; } = TestId;
    public int? DefaultValue { get; set; }
}