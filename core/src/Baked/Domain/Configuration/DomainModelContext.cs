using Baked.Domain.Model;
using Baked.Theme;

namespace Baked.Domain.Configuration;

public class DomainModelContext
{
    public required DomainModel Domain { get; init; }

    // NOTE this is intentionally left as null to make sure build fails if
    // there is a bug in setting this value
    public InspectTrace Trace { get; internal set; } = null!;
}