using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class DomainModelContext
{
    public required DomainModel Domain { get; init; }
}