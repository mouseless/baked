using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

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

    public static void VerifyInitialized(this IPhase phaseContext,
        ApplicationContext? context = default
    )
    {
        Mock.Get(phaseContext).Verify(p => p.Initialize(), Times.Once);

        if (context is not null)
        {
            phaseContext.Context.ShouldBeEquivalentTo(context);
        }
    }

    public static void VerifyNotInitialized(this IPhase phaseContext)
    {
        Mock.Get(phaseContext).Verify(p => p.Initialize(), Times.Never);
    }
}