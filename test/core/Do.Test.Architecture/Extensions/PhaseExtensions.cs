using Do.Architecture;
using Do.Testing;

namespace Do.Test;

public static class PhaseExtensions
{
    public static IPhase APhase(this Mocker mockMe,
        ApplicationContext? context = default,
        Func<bool>? isReady = default,
        Action? onInitialize = default,
        PhaseOrder order = PhaseOrder.Normal
    )
    {
        context ??= mockMe.Spec.GiveMe.AnApplicationContext();
        isReady ??= () => true;
        onInitialize ??= () => { };

        var result = new Mock<IPhase>();

        result.Setup(p => p.Order).Returns(order);

        result
            .Setup(p => p.IsReady)
            .Returns(() => isReady());

        result
            .Setup(p => p.Initialize())
            .Callback(() => onInitialize());

        result
            .Setup(p => p.Context)
            .Returns(context);

        return result.Object;
    }

    public static void VerifyInitialized(this IPhase source,
        ApplicationContext? context = default
    )
    {
        Mock.Get(source).Verify(p => p.Initialize(), Times.Once);

        if (context is not null)
        {
            source.Context.ShouldBeEquivalentTo(context);
        }
    }
}
