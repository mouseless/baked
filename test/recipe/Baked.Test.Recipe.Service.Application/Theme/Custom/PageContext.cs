using Baked.Domain.Model;

namespace Baked.Test.Theme.Custom;

public record PageContext
{
    public required DomainModel Domain { get; init; }
    public required Page Page { get; init; }
    public required List<Page> Sitemap { get; init; }
}