using Baked.Domain.Model;

using static Baked.Ui.UiLayer;

namespace Baked.Theme;

public record PageContext
{
    public required Route Route { get; init; }
    public required List<Route> Sitemap { get; init; }
    public required DomainModel Domain { get; init; }
    public required NewLocaleKey NewLocaleKey { get; init; }

    public void Deconstruct(out DomainModel domain, out NewLocaleKey l)
    {
        domain = Domain;
        l = NewLocaleKey;
    }

    public virtual ComponentContext Drill(string path) =>
        new()
        {
            Route = Route,
            Sitemap = Sitemap,
            Domain = Domain,
            NewLocaleKey = NewLocaleKey,
            Path = path
        };
}