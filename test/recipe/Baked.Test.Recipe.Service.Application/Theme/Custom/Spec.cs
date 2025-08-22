namespace Baked.Test.Theme.Custom;

public record Spec(string Name, IEnumerable<Spec.Link> Links)
{
    public record Link(string Title, string Description);
}