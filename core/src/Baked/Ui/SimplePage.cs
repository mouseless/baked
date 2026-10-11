namespace Baked.Ui;

public record SimplePage : PageSchemaBase
{
    public IComponentDescriptor Title { get; set; } = MissingComponent.Empty;
    public List<Content> Contents { get; init; } = [];
}