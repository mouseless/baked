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

    public static Parent AParentEntitiy(this Stubber giveMe, DateTime? dateTime = default) =>
        giveMe.A<Parent>().With(dateTime);

    public static Child AChildEntity(this Stubber giveMe, Parent? parent = default, DateTime? dateTime = default) =>
        giveMe.A<Child>().With(parent ?? giveMe.AParentEntitiy(), dateTime);

    public static IInterface AMockedObject(this Mocker mockMe, string[] results)
    {
        var mock = new Mock<IInterface>();

        var setup = () => mock.Setup(c => c.DoSomething());
        var taskSetup = () => mock.Setup(c => c.DoSomethingTask());

        setup().Returns(results);

        taskSetup().ReturnsAsync(results);

        return mock.Object;
    }
}
