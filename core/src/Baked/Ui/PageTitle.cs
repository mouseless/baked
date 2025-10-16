namespace Baked.Ui;

public record PageTitle(string Title)
    : IComponentSchema
{
    public string Title { get; set; } = Title;
    public string? Description { get; set; }
    public List<IComponentDescriptor> Actions { get; init; } = [];
}