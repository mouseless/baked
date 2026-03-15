using Baked.Architecture;
using Baked.Testing;

namespace Baked.Test;

public static class ApplicationExtensions
{
    extension(Stubber giveMe)
    {
        public Application AnApplication(
            IPhase? phase = default,
            IPhase[]? phases = default,
            ILayer? layer = default,
            ILayer[]? layers = default,
            IFeature? feature = default,
            IFeature[]? features = default,
            ApplicationContext? generateContext = default,
            ApplicationContext? startContext = default,
            RunFlags runFlags = RunFlags.Start
        )
        {
            layers ??= [layer ?? giveMe.Spec.MockMe.ALayer(startPhase: phase, startPhases: phases)];
            features ??= [feature ?? giveMe.Spec.MockMe.AFeature()];

            return giveMe.ABake(startContext: startContext, generateContext: generateContext, runflags: runFlags).Application(app =>
            {
                app.Layers.AddRange(layers);
                app.Features.AddRange(features);
            });
        }
    }
}