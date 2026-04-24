using Baked.Domain.Model;
using Baked.Theme;

namespace Baked.Domain.Configuration;

public class DomainModelContext
{
    public required DomainModel Domain { get; init; }
    public Inspect.Session Inspect { get; internal set; } = default!;
}