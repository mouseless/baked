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
       Status? @enum = default,
       DateTime? dateTime = default
    ) => giveMe
        .A<Entity>()
        .With(
            guid ?? Guid.NewGuid(),
            @string ?? string.Empty,
            stringData ?? string.Empty,
            int32 ?? 0,
            uri ?? giveMe.AUrl(),
            dynamic ?? new { },
            @enum ?? Status.Disabled,
            dateTime ?? new DateTime(year: 2023, month: 11, day: 29, hour: 18, minute: 30, second: 5, kind: DateTimeKind.Utc)
        );
}
