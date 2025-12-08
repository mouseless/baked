namespace Baked.Ui;

public record MissingComponent : IComponentSchema
{
    public List<string> Path { get; init; } = [];
    public DomainSource? Source { get; set; }
    public string? Component { get; set; }

    public record DomainSource(string Type)
    {
        public string Type { get; set; } = Type;
        public List<string> Path { get; init; } = [];
    }
}