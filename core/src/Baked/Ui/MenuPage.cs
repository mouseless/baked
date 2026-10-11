namespace Baked.Ui;

public record MenuPage : PageSchemaBase
{
    public string? FilterEvent { get; set; }
    public IComponentDescriptor? Header { get; set; }
    public List<Section> Sections { get; init; } = [];

    public record Section
    {
        public string? Title { get; set; }
        public List<Filterable> Links { get; init; } = [];
    }
}