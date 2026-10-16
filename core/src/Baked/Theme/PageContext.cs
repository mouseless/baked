using Baked.Domain.Inspection;
using Baked.Domain.Model;
using Baked.Ui;

namespace Baked.Theme;

public record PageContext
{
    public required Route Route { get; init; }
    public required IReadOnlyList<Route> Sitemap { get; init; }
    public required DomainModel Domain { get; init; }
    public required NewLocaleKey Localize { get; init; }

    // NOTE this is intentionally left as null to make sure build fails if
    // there is a bug in setting this value
    public Trace Trace { get; internal set; } = null!;

    public void Deconstruct(out DomainModel domain, out NewLocaleKey l)
    {
        domain = Domain;
        l = Localize;
    }

    public virtual ComponentContext Drill(params object[] paths) =>
        new()
        {
            Route = Route,
            Sitemap = Sitemap,
            Domain = Domain,
            Localize = Localize,
            Path = new(paths)
        };
}