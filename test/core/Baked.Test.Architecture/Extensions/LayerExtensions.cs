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
        IPhase? phase = default,
        IPhase[]? phases = default,
        Action? onApplyPhase = default,
        IPhase[]? generatePhases = default
    )
    {
        phaseContext ??= mockMe.Spec.GiveMe.APhaseContext(target: target, targets: targets);
        phases ??= [phase ?? mockMe.APhase()];
        generatePhases ??= [];

        var result = new Mock<ILayer>();
        result.Setup(l => l.GetPhases()).Returns(phases);
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
        Mock.Get(layer).Verify(l => l.GetPhases());

    public static void VerifyApplied(this ILayer layer, IPhase phase) =>
        Mock.Get(layer)
            .Verify(l => l.GetContext(
                phase,
                It.IsAny<ApplicationContext>()
            ));
}