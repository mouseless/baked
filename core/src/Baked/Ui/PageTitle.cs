namespace Baked.Ui;

public record PageTitle : IComponentSchema
{
    public string? TitleProp { get; set; }
    public bool? LocalizeTitle { get; set; }
    public IComponentDescriptor? Icon { get; set; }
    public List<Field>? InfoFields { get; set; }
    public string? Description { get; set; }
    public List<IComponentDescriptor> Actions { get; init; } = [];
    public int? EarlyWrapActionsAt { get; set; }
}