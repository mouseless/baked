using Do.Test.Orm;
using Do.Testing;

namespace Do.Test;

public static class EntityExtensions
{
    public static Entity AnEntity(this Stubber giveMe,
       Guid? guid = default,
       string? @string = default,
       string? stringData = default,
       int? int32 = default,
       string? unique = default,
       Uri? uri = default,
       object? @dynamic = default,
       Status? @enum = default,
       DateTime? dateTime = default,
       bool? setNowForDateTime = false
    ) => giveMe
        .A<Entity>()
        .With(
            guid ?? giveMe.AGuid(),
            @string ?? string.Empty,
            stringData ?? string.Empty,
            int32 ?? 0,
            unique ?? $"giveMe.AGuid()",
            uri ?? giveMe.AUrl(),
            dynamic ?? new { },
            @enum ?? Status.Disabled,
            dateTime ?? giveMe.ADateTime(),
            setNowForDateTime ?? false
        );
}
