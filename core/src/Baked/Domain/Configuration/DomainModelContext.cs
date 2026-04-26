using Baked.Domain.Model;
using Baked.Theme;

namespace Baked.Domain.Configuration;

public abstract class DomainModelContext
{
    public required DomainModel Domain { get; init; }
    public abstract string Identifier { get; }

    // NOTE this is intentionally left as null to make sure build fails if
    // there is a bug in setting this value
    public InspectTrace Trace { get; internal set; } = null!;
}