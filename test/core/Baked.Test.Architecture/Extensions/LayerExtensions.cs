using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

public static class LayerExtensions
{
    public static ILayer ALayer(this Mocker mockMe,
        string? id = default,
        object? target = default,
        object[]? targets = default,
        PhaseContext? phaseContext = default,
        IPhase? startPhase = default,
        IPhase[]? startPhases = default,
        Action? onApplyPhase = default,
        IPhase[]? generatePhases = default
    )
    {
        phaseContext ??= mockMe.Spec.GiveMe.APhaseContext(target: target, targets: targets);
        startPhases ??= [startPhase ?? mockMe.APhase()];
        generatePhases ??= [];

        var result = new Mock<ILayer>();
        result.Setup(l => l.GetStartPhases()).Returns(startPhases);
        result.Setup(l => l.GetGeneratePhases()).Returns(generatePhases);
        result.Setup(l => l.Id).Returns(id ?? $"{Guid.NewGuid()}");

        var setupGetContext = result
            .Setup(l => l.GetContext(It.IsAny<IPhase>(), It.IsAny<ApplicationContext>()))
            .Returns(phaseContext);

        if (onApplyPhase != default)
        {
            setupGetContext.Callback((IPhase _, ApplicationContext _) => onApplyPhase());
        }

        return result.Object;
    }

    public static void VerifyInitialized(this ILayer layer) =>
        Mock.Get(layer).Verify(l => l.GetStartPhases());

    public static void VerifyApplied(this ILayer layer, IPhase phase) =>
        Mock.Get(layer)
            .Verify(l => l.GetContext(
                phase,
                It.IsAny<ApplicationContext>()
            ));
}