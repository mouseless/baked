using Baked.Ui;

namespace Baked.Theme.Admin;

public record Message : IComponentSchema
{
    public string Severity { get; set; } = "info";
    public string? Icon { get; set; }
    public bool? LocalizeMessage { get; set; }
}