using Baked.Domain.Model;
using Baked.Ui;

namespace Baked.Theme;

public record PageContext
{
    public required Route Route { get; init; }
    public required IReadOnlyList<Route> Sitemap { get; init; }
    public required DomainModel Domain { get; init; }
    public required NewLocaleKey NewLocaleKey { get; init; }

    // NOTE this is intentionally left as null to make sure build fails if
    // there is a bug in setting this value
    public InspectTrace Trace { get; internal set; } = null!;

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