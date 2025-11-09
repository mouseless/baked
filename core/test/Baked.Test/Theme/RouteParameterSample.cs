using Baked.Authorization;

namespace Baked.Test.Theme;

[AllowAnonymous]
public class RouteParameterSample
{
    public RouteParameterSample With(string id)
    {
        Id = id;

        return this;
    }

    public string Id { get; set; } = default!;

    public List<Item> GetItems() =>
        [.. Enumerable.Repeat(0, 10).Select((_, index) => new Item($"{index}", Id))];

    public record Item(string Id, string Value);
}