using Moq;

namespace Baked.Testing;

public record MockDescriptor(
    Type Type,
    bool Singleton = false,
    Action<Mock>? Setup = default
);