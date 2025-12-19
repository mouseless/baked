namespace Baked.Ui;

public record Tab(string Id)
    : ISupportsReaction
{
    public string Id { get; set; } = Id;
    public string? Title { get; set; }
    public List<Content> Contents { get; init; } = [];
    public bool? FullScreen { get; set; }
    public IComponentDescriptor? Icon { get; set; }
    public Dictionary<string, ITrigger>? Reactions { get; set; }
}