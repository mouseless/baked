using Baked.Ui;

namespace Baked.Theme.Admin;

public record LanguageSwitcher : IComponentSchema
{
    public string? Direction { get; set; }
}