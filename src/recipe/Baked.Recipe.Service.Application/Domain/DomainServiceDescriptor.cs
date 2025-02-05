using Baked.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Domain;

public record DomainServiceDescriptor(
    TypeModel ServiceType,
    ServiceLifetime Lifetime,
    bool UseFactory,
    IEnumerable<TypeModelReference> Interfaces,
    bool Forward
);