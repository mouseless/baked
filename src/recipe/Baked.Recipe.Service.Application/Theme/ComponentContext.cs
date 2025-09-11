namespace Baked.Theme;

public record ComponentContext : PageContext
{
    public required string Path { get; init; }

    public override ComponentContext Drill(string path) =>
        this with { Path = $"{Path}{path}" };
}