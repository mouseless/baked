using Baked.Domain.Model;

using static Baked.Ui.UiLayer;

namespace Baked.Test.Theme.Custom;

public record PageContext
{
    public required Page Page { get; init; }
    public required List<Page> Sitemap { get; init; }
    public required DomainModel Domain { get; init; }
    public required NewLocaleKey NewLocaleKey { get; init; }

    public void Deconstruct(out DomainModel domain, out NewLocaleKey l)
    {
        domain = Domain;
        l = NewLocaleKey;
    }
}