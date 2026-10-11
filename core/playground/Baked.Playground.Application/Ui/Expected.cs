using Baked.Ui;

namespace Baked.Playground.Ui;

public record Expected : IComponentSchema
{
    public string TestId { get; set; } = string.Empty;
    public bool? ShowDataParams { get; set; }
}