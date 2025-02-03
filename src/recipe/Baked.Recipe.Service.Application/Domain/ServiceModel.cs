using Baked.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Domain;

public record ServiceModel(
    TypeModel ServiceType,
    ServiceLifetime Lifetime,
    bool UseFactory,
    IEnumerable<TypeModelReference> Interfaces,
    bool Forward
);