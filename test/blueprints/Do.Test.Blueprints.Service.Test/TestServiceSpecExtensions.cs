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
       DateTime? dateTime = default,
       bool? setNowForDateTime = false
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
            dateTime ?? giveMe.ADateTime(),
            setNowForDateTime ?? false
        );

    public static Parent AParent(this Stubber giveMe,
        string? name = default
    )
    {
        name ??= giveMe.AString();

        return giveMe.A<Parent>().With(name);
    }

    public static void ShouldThrowExceptionWithServiceNotRegisteredMessage(this Func<object> source, Type serviceType) =>
        source.ShouldThrow<Exception>().Message.ShouldBe($"No service for type '{serviceType}' has been registered.");
}
