using Baked.Domain.Model;
using Baked.Ui;

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

    public virtual ComponentContext Drill(params object[] paths) =>
        new()
        {
            Route = Route,
            Sitemap = Sitemap,
            Domain = Domain,
            NewLocaleKey = NewLocaleKey,
            Path = new(paths)
        };
}