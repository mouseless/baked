namespace Baked.Theme;

public record ComponentContext : PageContext
{
    public required ComponentPath Path { get; init; }

    public override ComponentContext Drill(params object[] paths) =>
        this with { Path = Path.Drill(paths) };
}