namespace Baked.Ui;

public record Tab : ISupportsReaction, IOrderableSchema
{
    public string Id { get; set; } = string.Empty;
    public string? Title { get; set; }
    public List<Content> Contents { get; init; } = [];
    public bool? FullScreen { get; set; }
    public IComponentDescriptor? Icon { get; set; }
    public Dictionary<string, ITrigger>? Reactions { get; set; }

    string IOrderableSchema.Key => Id;
}