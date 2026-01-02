using Baked.Authorization;

namespace Baked.Playground.Theme;

[AllowAnonymous]
public class RouteParametersSample
{
    public RouteParametersSample With(string id)
    {
        Id = id;

        return this;
    }

    public string Id { get; set; } = default!;

    public List<Item> GetItems() =>
        [.. Enumerable.Repeat(0, 10).Select((_, index) => new Item($"{index}", $"{Id} - {index}"))];
}