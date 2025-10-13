using Baked.Ui;

namespace Baked.Test.Theme.Custom;

public record Expected(string TestId)
    : IComponentSchema
{
    public string TestId { get; set; } = TestId;
    public bool? ShowDataParams { get; set; }
}