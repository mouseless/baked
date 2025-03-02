using Baked.Ui;

namespace Baked.Theme.Admin;

public record PageTitle(string Title)
    : IComponentSchema
{
    public string Title { get; set; } = Title;
    public string? Description { get; set; }
    public List<IComponentSchema> Actions { get; init; } = [];
}