using Baked.Authorization;
using Baked.Business;

namespace Baked.Playground.Theme;

[AllowAnonymous]
public class RouteParametersSample
{
    public RouteParametersSample With(Id id)
    {
        Id = id;

        return this;
    }

    public Id Id { get; set; } = default!;

    public List<Item> GetItems() =>
        [.. Enumerable.Repeat(0, 10).Select((_, index) => new Item($"{index}", $"{Id} - {index}"))];
}