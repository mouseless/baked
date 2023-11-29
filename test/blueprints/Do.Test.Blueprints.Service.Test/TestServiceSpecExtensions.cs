using Do.Testing;

namespace Do.Test;

public static class TestServiceSpecExtensions
{
    public static Entity AnEntity(this Stubber giveMe,
       Guid? guid = default,
       string? @string = default,
       string? stringData = default,
       int? int32 = default,
       Uri? uri = default,
       object? @dynamic = default,
       Status? status = default
    ) => giveMe
        .A<Entity>()
        .With(
            guid ?? Guid.NewGuid(),
            @string ?? string.Empty,
            stringData ?? string.Empty,
            int32 ?? 0, uri ?? giveMe.AUrl(),
            dynamic ?? new { },
            status ?? Status.Disabled
        );
}
