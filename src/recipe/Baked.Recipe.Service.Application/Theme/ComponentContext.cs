namespace Baked.Theme;

public record ComponentContext : PageContext
{
    public required string Path { get; init; }

    public override ComponentContext CreateComponentContext(string path) =>
        this with { Path = $"{Path}{path}" };
}