using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

public static class LayerExtensions
{
    extension(Mocker mockMe)
    {
        public ILayer ALayer(
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
    }

    extension(ILayer layer)
    {
        public void VerifyInitialized() =>
            Mock.Get(layer).Verify(l => l.GetStartPhases());

        public void VerifyApplied(IPhase phase) =>
            Mock.Get(layer)
                .Verify(l => l.GetContext(
                    phase,
                    It.IsAny<ApplicationContext>()
                ));
    }
}