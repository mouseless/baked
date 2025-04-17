using Baked.Ui;

namespace Baked.Theme.Admin;

public record Message(string Severity, string Icon)
    : IComponentSchema
{
    public string Severity { get; init; } = Severity;
    public string Icon { get; init; } = Icon;
}
